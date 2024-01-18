using DataAccessLayer.Data;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Models.Models.DTOs.PrimaryClientDetailsDTOs;
using Models.Models.DTOs.UserDetailsDTOs;
using Sugamta.API.DTOs.UserDetailsDTOs;
using Sugamta.API.Repository.Interface;

namespace Sugamta.API.Controllers
{
    [Route("api/")]
    [ApiController]
    public class PrimaryClientDetailsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserDbContext _dbContext;

        public PrimaryClientDetailsController(IUnitOfWork unitOfWork, UserDbContext dbContext)
        {
            _unitOfWork = unitOfWork;
            _dbContext= dbContext;
        }

        [HttpGet("get-primary-client-details/{email}")]
        public ActionResult GetPrimaryClientDetails(string email)
        {
            try
            {
                //var primaryClient = _unitOfWork.PrimaryClient.GetPrimaryClientByEmail(email);
                var userDetails = _unitOfWork.PrimaryClientDetails.GetPrimaryClientDetailsByEmail(email);

                if (userDetails == null)
                {
                    PrimaryClientDetailsEditDto primaryClientDetailsEditDto = new PrimaryClientDetailsEditDto();
                    return NotFound(primaryClientDetailsEditDto);
                }

                var primaryClientDto = userDetails.Adapt<PrimaryClientDetailsEditDto?>();
                var existingCountry = _unitOfWork.Country.GetCountries();
                if (existingCountry == null)
                {
                    return BadRequest("Country Not Found.");
                }
                var existingState = _unitOfWork.State.GetStates();
                if (existingState == null)
                {
                    return BadRequest("State Not Found");
                }

                if(primaryClientDto.CountryId != 0)
                {
                    for(var i = 0; i < existingCountry.Count; i++)
                    {
                        if(userDetails.CountryId == existingCountry[i].CountryId)
                        {
                            primaryClientDto.Country = existingCountry[i].CountryName;
                        }
                    }
                }

                if (primaryClientDto.StateId != 0)
                {
                    for (var i = 0; i < existingState.Count; i++)
                    {
                        if (userDetails.StateId == existingState[i].StateId)
                        {
                            primaryClientDto.State = existingState[i].StateName;
                        }
                    }
                }

                return Ok(primaryClientDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to retrieve Primary Client Details: {ex.Message}");
            }
        }

        [HttpGet("get-primary-client-details-for-create-or-update/{email}")]
        public IActionResult GetPrimaryClientDetailsForCreateorUpdate(string email)
        {
            try
            {
                var primaryClient = _unitOfWork.PrimaryClientDetails.GetPrimaryClientDetailsByEmail(email);
                var primaryClientDetailsDto = primaryClient.Adapt<PrimaryClientDetailsDto>();
                if (primaryClient == null)
                {
                    return NotFound();
                }
                var existingCountry = _unitOfWork.Country.GetCountries();
                if (existingCountry == null)
                {
                    return BadRequest("Country Not Found.");
                }
                var existingState = _unitOfWork.State.GetStates();
                if (existingState == null)
                {
                    return BadRequest("State Not Found");
                }
                return Ok(primaryClientDetailsDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to retrieve Primary Client Details: {ex.Message}");
            }
        }

        [HttpGet("get-primary-client-image/{email}")]
        public IActionResult DisplayPrimaryClientImage(string email)
        {
            try
            {
                var primaryClientDetails = _unitOfWork.PrimaryClientDetails.GetPrimaryClientDetailsByEmail(email);

                if (primaryClientDetails == null || primaryClientDetails.ImageUrl == null || primaryClientDetails.ImageUrl.Length == 0)
                {
                    return NotFound();
                }

                var base64String = Convert.ToBase64String(primaryClientDetails.ImageUrl);

                // Return the base64-encoded string as part of the response
                return Ok(base64String);
            }
            catch (Exception ex)
            {
                // Handle exceptions, log errors, etc.
                return StatusCode(500, $"Failed to retrieve and display image: {ex.Message}");
            }
        }

        [HttpPost("add-primary-client-details")]
        public ActionResult AddPrimaryClientDetails([FromForm] PrimaryClientDetailsCreateOrUpdateDto _primaryClientDetailsDto)
        {
            try
            {
                var primaryClientDetailsDto = _primaryClientDetailsDto.Adapt<PrimaryClientDetailsDto>();
                var existingClient = _unitOfWork.PrimaryClientDetails.GetPrimaryClientDetailsByEmail(primaryClientDetailsDto.PrimaryClientEmail);
                if (existingClient != null)
                {
                    return BadRequest("This UserDetails already added. Please go for updating UserDetails.");
                }


                if (_primaryClientDetailsDto.formFile != null)
                {


                    using (var memoryStream = new MemoryStream())
                    {
                        _primaryClientDetailsDto.formFile.CopyToAsync(memoryStream);
                        var imageBytes = memoryStream.ToArray();

                        primaryClientDetailsDto.ImageUrl = imageBytes;
                    }
                }

                var existingCountry = _unitOfWork.Country.GetCountries();
                if (existingCountry == null)
                {
                    return BadRequest("Country Not Found.");
                }
                var existingState = _unitOfWork.State.GetStates();
                if (existingState == null)
                {
                    return BadRequest("State Not Found");
                }
                primaryClientDetailsDto.CreationDate = DateTime.Now;
                _unitOfWork.PrimaryClientDetails.InsertPrimaryClientDetails(primaryClientDetailsDto);
                _unitOfWork.Save();
                return Ok("Primary Client Details added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to Add Primary Client Details: {ex.Message}");
            }
        }

        [HttpPut("update-primary-client-details")]
        public ActionResult UpdatePrimaryClientDetails([FromForm] PrimaryClientDetailsCreateOrUpdateDto primaryClientDetails)
        {
            try
            {
                var primaryClientDetailsDto = primaryClientDetails.Adapt<PrimaryClientDetailsDto>();

                if (primaryClientDetails.formFile != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        primaryClientDetails.formFile.CopyToAsync(memoryStream);
                        var imageBytes = memoryStream.ToArray();

                        primaryClientDetailsDto.ImageUrl = imageBytes;
                    }
                }
                var existingCountry = _unitOfWork.Country.GetCountries();
                if (existingCountry == null)
                {
                    return BadRequest("Country Not Found.");
                }
                var existingState = _unitOfWork.State.GetStates();
                if (existingState == null)
                {
                    return BadRequest("State Not Found");
                }

                var existingUser = _unitOfWork.PrimaryClientDetails.GetPrimaryClientDetailsByEmail(primaryClientDetailsDto.PrimaryClientEmail);
                _dbContext.Entry(existingUser).State = EntityState.Detached;
                primaryClientDetailsDto.CreationDate = existingUser.CreationDate;
                primaryClientDetailsDto.UpdationDate = DateTime.Now;
                _unitOfWork.PrimaryClientDetails.UpdatePrimaryClientDetails(primaryClientDetailsDto);
                _unitOfWork.Save();
                return Ok("Primary Client Details updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to update Primary Client Details: {ex.Message}");
            }
        }

        [HttpDelete("delete-primary-client-details/{email}")]
        public ActionResult DeletePrimaryClientDetails(string email)
        {
            try
            {
                var primaryClientDetailsDto = _unitOfWork.PrimaryClientDetails.GetPrimaryClientDetailsByEmail(email);
                if (primaryClientDetailsDto == null)
                {
                    return NotFound($"Primary Client Details with email '{email}' not found.");
                }

                var primaryClientDetails = primaryClientDetailsDto.Adapt<Models.Models.PrimaryClientDetails>(); // Adapt to entity

                _unitOfWork.PrimaryClientDetails.DeletePrimaryClientDetails(primaryClientDetails);
                _unitOfWork.Save();
                return Ok("Primary Client Details deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to delete PrimaryClientDetails: {ex.Message}");
            }
        }
    }
}
