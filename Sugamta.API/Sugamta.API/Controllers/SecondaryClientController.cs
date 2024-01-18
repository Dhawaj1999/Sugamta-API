

using DataAccessLayer.Data;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Models.Models.DTOs.SecondaryClientDetailsDTOs;
using Models.Models.DTOs.SecondaryClientDTOs;
using Sugamta.API.DTOs.UserDTOs;
using Sugamta.API.Repository.Interface;

namespace Sugamta.API.Controllers
{
    [ApiController]
    [Route("api")]
    public class SecondaryClientController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserDbContext _userDbContext;

        public SecondaryClientController(IUnitOfWork unitOfWork, UserDbContext userDbContext)
        {
            _unitOfWork = unitOfWork;
            _userDbContext = userDbContext;
        }

        [HttpGet("get-all-secondary-client")]
        public IActionResult GetAllSecondaryClient()
        {
            try
            {
                var secondaryClientList = _unitOfWork.SecondaryClient.GetSecondaryClientsList();
                var secondaryClientDtoList = secondaryClientList?.Select(d => d.Adapt<SecondaryClientDto>()).ToList();

                foreach (var item in secondaryClientDtoList)
                {
                    var role = _unitOfWork.Role.GetRoleById(i => i.RoleId == item.RoleId);
                    item.RoleType = role.RoleType;
                }
                return Ok(secondaryClientDtoList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to retrieve all SecondaryClientDetails: {ex.Message}");
            }
        }

        [HttpGet("get-secondary-client/{email}")]
        public ActionResult GetSecondaryClients(string email)
        {
            try
            {
                var secondaryClient = _unitOfWork.SecondaryClient.GetSecondaryClients(email);
                var secondaryClientDto = secondaryClient.Adapt<SecondaryClientDto>();
                if (secondaryClientDto == null)
                {
                    return NotFound("This Email Not found the details of SecondaryClient");
                }
                return Ok(secondaryClient);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPost("add-secondary-client-records")]
        public ActionResult AddSecondaryClients(SecondaryClientCreateDTOs clientDto)
        {
            try
                 
            {
                clientDto.Password = BCrypt.Net.BCrypt.HashPassword(clientDto.Password);
                var secondaryClientDto = clientDto.Adapt<SecondaryClientDto>();
                _unitOfWork.SecondaryClient.InsertSecondaryClients(secondaryClientDto);
                _unitOfWork.Save();
                return Ok("Secondary client added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPut("update-secondary-client")]
        public ActionResult UpdateSecondary(SecondaryClientDto clientDto)
        {
            try
            {
                var secondaryClientDto = clientDto.Adapt<SecondaryClientDto>();
                _unitOfWork.SecondaryClient.UpdateSecondaryClient(clientDto);
                _unitOfWork.Save();
                return Ok("Update successful");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpDelete("delete-secondary-records/{email}")]
        public ActionResult DeleteClientRecord(string email)
        {
            try
            {

                var secondaryClient = _unitOfWork.SecondaryClient.GetSecondaryClients(email);

                _unitOfWork.SecondaryClient.DeleteSecondaryClient(secondaryClient);
                _unitOfWork.Save();
                return Ok(new { Message = "Secondary client record deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
