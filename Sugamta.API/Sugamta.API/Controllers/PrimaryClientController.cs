using DataAccessLayer.Data;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Models.Models.DTOs.PrimaryClientDTOs;
using Sugamta.API.DTOs.UserDTOs;
using Sugamta.API.Repository;
using Sugamta.API.Repository.Interface;

namespace Sugamta.API.Controllers
{
    [Route("api/")]
    [ApiController]
    public class PrimaryClientController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserDbContext _dbContext;

        public PrimaryClientController(IUnitOfWork unitOfWork, UserDbContext dbContext)
        {
            _unitOfWork = unitOfWork;
            _dbContext = dbContext;
        }

        [HttpGet("get-primary-client-all")]
        public IActionResult GetPrimaryClient()
        {
            try
            {
                var result = _unitOfWork.PrimaryClient.GetPrimaryClient();
                var data = result.Adapt<List<PrimaryClientDto>>();
                foreach (var item in data)
                {
                    var agency = _unitOfWork.Agency.GetAgencyByEmail(item.AgencyEmail);
                    item.AgencyName = agency.Name;
                }
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to retrieve PrimaryClientDetails: {ex.Message}");
            }
        }

        [HttpGet("get-primary-client-by-email/{email}")]
        public ActionResult GetPrimaryClientbyEmail(string email)
        {
            try
            {
                var primaryClient = _unitOfWork.PrimaryClient.GetPrimaryClientByEmail(email);
                var primaryClientDto = primaryClient.Adapt<PrimaryClientCreateDto>();
                if (primaryClient == null)
                {
                    return NotFound("This Email Not found the details of PrimaryClient");
                }
                return Ok(primaryClientDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to retrieve PrimaryClientDetails: {ex.Message}");
            }
        }

        [HttpPost("add-primary-client")]
        public ActionResult AddPrimaryClient(PrimaryClientCreateDto primaryClientCreateDto)
        {
            try
            {
                // var primaryClientDto = primaryClientCreateDto.Adapt<PrimaryClientCreateDto>();

                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(primaryClientCreateDto.Password);
                primaryClientCreateDto.Password = hashedPassword;
                primaryClientCreateDto.CreationDate = DateTime.Now;
                _unitOfWork.PrimaryClient.InsertPrimaryClient(primaryClientCreateDto);
                return Ok("PrimaryClient Added Successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to Add PrimaryClientDetails: {ex.Message}");
            }
        }

        [HttpPut("update-primary-client")]
        public ActionResult UpdatePrimaryClient(PrimaryClientUpdateDto primaryClientDto)
        {
            try
            {
                //var primaryClient=primaryClientDto.Adapt<PrimaryClient>();
                // var existingClient = _unitOfWork.PrimaryClient.GetPrimaryClientByEmail(primaryClient.ClientEmail);
                // _dbContext.Entry(existingClient).State = EntityState.Detached;

                _unitOfWork.PrimaryClient.UpdatePrimaryClient(primaryClientDto);
                return Ok("PrimaryClient Updated Successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to Update PrimaryClientDetails: {ex.Message}");
            }
        }

        [HttpDelete("delete-primary-client/{email}")]
        public ActionResult DeletePrimaryClient(string email)
        {
            try
            {
                var primaryClientDto = _unitOfWork.PrimaryClient.GetPrimaryClientByEmail(email);
                if(primaryClientDto == null)
                {
                    return NotFound();
                }
                var primaryClientDetails = primaryClientDto.Adapt<PrimaryClient>();
                _unitOfWork.PrimaryClient.DeletePrimaryClient(primaryClientDetails);
                return Ok("PrimaryClient Deleted Successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to Delete PrimaryClientDetails: {ex.Message}");
            }
        }
    }
}
