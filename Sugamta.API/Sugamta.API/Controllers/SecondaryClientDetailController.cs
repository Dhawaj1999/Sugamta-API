using DataAccessLayer.Data;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using Models.Models;
using Models.Models.DTOs.SecondaryClientDetailsDTOs;
using Models.Models.DTOs.SecondaryClientDTOs;
using Models.Models.DTOs.UserDetailsDTOs;
using Sugamta.API.DTOs.UserDetailsDTOs;
using Sugamta.API.DTOs.UserDTOs;
using Sugamta.API.Repository.Interface;

namespace Sugamta.API.Controllers
{
    [ApiController]
    [Route("api/secondary-client")]
    public class SecondaryClientDetailController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserDbContext _userDbContext;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public SecondaryClientDetailController(IUnitOfWork unitOfWork, UserDbContext userDbContext, IWebHostEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _userDbContext = userDbContext;
            _hostingEnvironment = hostingEnvironment;
        }


        [HttpGet("secondory-client-image/{email}")]
        public IActionResult DisplayImage(string email)
        {
            try
            {
                var secondaryClientDetail = _unitOfWork.SecondaryClientDetail.GetSecondaryClientDetail(email);

                if (secondaryClientDetail == null || secondaryClientDetail.ImageUrl == null || secondaryClientDetail.ImageUrl.Length == 0)
                {
                    return NotFound();
                }

                var base64String = Convert.ToBase64String(secondaryClientDetail.ImageUrl);

                // Return the base64-encoded string as part of the response
                return Ok(base64String);
            }
            catch (Exception ex)
            {
                // Handle exceptions, log errors, etc.
                return StatusCode(500, $"Failed to retrieve and display image: {ex.Message}");
            }
        }
       

        [HttpGet("get-secondary-client-details/{email}")]
        public ActionResult GetSecondaryClientDetails(string email)
        {
            try
            {
                var secondaryClientDetails = _unitOfWork.SecondaryClientDetail.GetSecondaryClientDetail(email);
                

                if (secondaryClientDetails == null)
                {
                    SecondaryClientDetailsEditDtos secondaryClientDetailsEditDtos= new SecondaryClientDetailsEditDtos();
                    return NotFound(secondaryClientDetailsEditDtos);
                }


                //var secondaryClientDetailsDto = secondaryClientDetails.Adapt<SecondaryClientDetailsEditDtos?>();

                SecondaryClientDetailsEditDtos secondaryClientDetailsDto = new()
                {
                       SecondaryClientEmail = secondaryClientDetails.SecondaryClientEmail,
                     //  Name = secondaryClientDetails.Name,
                       Address = secondaryClientDetails.Address,
                      // Gender = secondaryClientDetails.Gender,
                     //  Age = secondaryClientDetails.Age,
                       City = secondaryClientDetails.City,
                       StateId = secondaryClientDetails.StateId,
                       CountryId = secondaryClientDetails.CountryId,
                       PhoneNumber = secondaryClientDetails.PhoneNumber,
                       AlternatePhoneNumber = secondaryClientDetails.AlternatePhoneNumber,
                       CreationDate = secondaryClientDetails.CreationDate,
                       UpdationDate = secondaryClientDetails.UpdationDate,
                       ImageUrl = secondaryClientDetails.ImageUrl
                };

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

                if (secondaryClientDetailsDto.CountryId != 0)
                {
                    for(var i=0; i<existingCountry.Count; i++)
                    {
                        if(secondaryClientDetails.CountryId == existingCountry[i].CountryId)
                        {
                            secondaryClientDetailsDto.Country = existingCountry[i].CountryName;
                        }
                    }
                }

                if (secondaryClientDetailsDto.StateId != 0)
                {
                    for (var i = 0; i < existingState.Count; i++)
                    {
                        if (secondaryClientDetails.StateId == existingState[i].StateId)
                        {
                            secondaryClientDetailsDto.State = existingState[i].StateName;
                        }
                    }
                }

                // Fetch other required data (countries, states, etc.)

                return Ok(secondaryClientDetailsDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to retrieve SecondaryClientDetails: {ex.Message}");
            }
        }

       

       [HttpPost("add-secondary-client-details")]
        public ActionResult AddSecondaryClientDetails([FromForm] SecondaryClientDetailsCreateOrUpdateDto _secondaryClientDetailsDtos)
        {
            try
            {
                var secondaryClientDetailsDtos = _secondaryClientDetailsDtos.Adapt<SecondaryClientDetailsDtos>();

                var existingClient = _unitOfWork.SecondaryClientDetail.GetSecondaryClientDetail(secondaryClientDetailsDtos.SecondaryClientEmail);

                if (existingClient != null)
                {
                    return BadRequest("This UserDetails already added. Please go for updating UserDetails.");
                }

                if (_secondaryClientDetailsDtos.formFile != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        _secondaryClientDetailsDtos.formFile.CopyToAsync(memoryStream);
                        var imageBytes = memoryStream.ToArray();

                        secondaryClientDetailsDtos.ImageUrl = imageBytes;
                    }
                }

                // Checking the existence of countries and states
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

                // Setting the creation date
               secondaryClientDetailsDtos.CreationDate = DateTime.Now;

                // Inserting secondary client details into the database
                _unitOfWork.SecondaryClientDetail.InsertSecondaryclientDetails(secondaryClientDetailsDtos);
                _unitOfWork.Save();

                return Ok("SecondaryClientDetails added successfully");
            }
            catch (Exception ex)
            {
                // Handling exceptions and returning an error response
                return StatusCode(500, $"Failed to add SecondaryClientDetails: {ex.Message}");
            }
        }

        [HttpPut("update-secondary-client-details")]
        public ActionResult UpdateSecondaryClientDetails([FromForm] SecondaryClientDetailsCreateOrUpdateDto secondaryClientDetailsDto)
        {
            try
            {
                var secondaryClientDetailsDtos = secondaryClientDetailsDto.Adapt<SecondaryClientDetailsDtos>();


               /* if (existingClient != null)
                {
                    return BadRequest("This UserDetails already added. Please go for updating UserDetails.");
                }*/

                if (secondaryClientDetailsDto.formFile != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        secondaryClientDetailsDto.formFile.CopyToAsync(memoryStream);
                        var imageBytes = memoryStream.ToArray();

                        secondaryClientDetailsDtos.ImageUrl = imageBytes;
                    }
                }

                // Checking the existence of countries and states
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
                var existingClient = _unitOfWork.SecondaryClientDetail.GetSecondaryClientDetail(secondaryClientDetailsDtos.SecondaryClientEmail);
                _userDbContext.Entry(existingClient).State = EntityState.Detached;
               //  secondaryClientDetailsDtos.CreationDate = existingClient.CreationDate;
                secondaryClientDetailsDtos.UpdationDate = DateTime.Now;
                _unitOfWork.SecondaryClientDetail.UpdateSecondaryClientDetails(secondaryClientDetailsDtos);
                _unitOfWork.Save();

                return Ok("SecondaryClientDetails Updates successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to add SecondaryClientDetails: {ex.Message}");
            }
        }


       

        [HttpDelete("delete-secondary-client-details/{email}")]
        public ActionResult DeleteSecondaryClientDetails(string email)
        {
            try
            {
                _unitOfWork.SecondaryClientDetail.DeleteSecondaryClientDetails(email);

                return Ok("SecondaryClientDetails deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to delete SecondaryClientDetails: {ex.Message}");
            }
        }

        
    }
}
