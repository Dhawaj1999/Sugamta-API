using DataAccessLayer.Data;
//using DataAccessLayer.Migrations;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Models.Models;
using Models.Models.DTOs.UserDetailsDTOs;
using Sugamta.API.DTOs.UserDetailsDTOs;
using Sugamta.API.Repository;
using Sugamta.API.Repository.Interface;
using System;

namespace Sugamta.API.Controllers
{
    [ApiController]
    [Route("api")]
    public class UserDetailsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserDbContext _userDbContext;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public UserDetailsController(IUnitOfWork unitOfWork, UserDbContext userDbContext, IWebHostEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _userDbContext = userDbContext;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet("get-profile-image/{email}")]
        public IActionResult DisplayImage(string email)
        {
            try
            {
                var userDetails = _unitOfWork.UserDetails.GetUserDetails(email);

                if (userDetails == null || userDetails.ImageUrl == null || userDetails.ImageUrl.Length == 0)
                {
                    return NotFound();
                }

                var base64String = Convert.ToBase64String(userDetails.ImageUrl);

                // Return the base64-encoded string as part of the response
                return Ok(base64String);
            }
            catch (Exception ex)
            {
                // Handle exceptions, log errors, etc.
                return StatusCode(500, $"Failed to retrieve and display image: {ex.Message}");
            }
        }

        [HttpGet("get-country-list")]
        public IActionResult GetCountry()
        {
            var data=_unitOfWork.Country.GetCountries();
            return Ok(data);
        }
        [HttpGet("get-state-list")]
        public IActionResult GetState()
        {
            var state=_unitOfWork.State.GetStates();
            return Ok(state);
        }


        [HttpGet("get-user-details/{email}")]
        public ActionResult GetUserDetails(string email)
        {
            try
            {
                var user = _unitOfWork.user.GetUser(email);
                var userDetails = _unitOfWork.UserDetails.GetUserDetails(email);
                //var userDetailsDto = userDetails.Adapt<UserDetailsDto>();
                var userDto = user.Adapt<UserDetailsDto>();
                if (userDetails == null)
                {
                    return NotFound(userDto.Email);
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

                return Ok(userDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to retrieve UserDetails: {ex.Message}");
            }
        }

        [HttpGet("get-user-details-for-create-or-update/{email}")]
        public ActionResult GetUserDetailsForCreateOrUpdate(string email)
        {
            try
            {
                var userDetails = _unitOfWork.UserDetails.GetUserDetails(email);
                var userDetailsDto = userDetails.Adapt<UserDetailsDto>();
                if (userDetails == null)
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
                return Ok(userDetailsDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to retrieve UserDetails: {ex.Message}");
            }
        }

        [HttpPost("add-user-details")]
        public ActionResult AddUserDetails([FromForm] UserDetailsCreateOrUpdateDto _userDetailsDto)
        {
            try
            {
                var userDetailsDto = _userDetailsDto.Adapt<UserDetailsDto>();
                var existingUser = _unitOfWork.UserDetails.GetUserDetails(userDetailsDto.Email);
                if (existingUser != null)
                {
                    return BadRequest("This UserDetails already added. Please go for updating UserDetails.");
                }
               

                if(_userDetailsDto.formFile != null)
                {
                   

                    using (var memoryStream = new MemoryStream())
                    {
                        _userDetailsDto.formFile.CopyToAsync(memoryStream);
                        var imageBytes = memoryStream.ToArray();

                        var uploadDirectory = Path.Combine(_hostingEnvironment.WebRootPath, "UploadedImages");
                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(_userDetailsDto.formFile.FileName);
                        var imagePath = Path.Combine(uploadDirectory, fileName);

                        userDetailsDto.ImageUrl = imageBytes;
                        userDetailsDto.ImageLocalPath = uploadDirectory;

                        System.IO.File.WriteAllBytesAsync(imagePath, imageBytes);
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

               // userDetailsDto.CountryId =_userDetailsDto.CountryId;
                //userDetailsDto.StateId =_userDetailsDto.StateId;
                userDetailsDto.CreationDate = DateTime.Now;
                //UserDetails userDetails = userDetailsDto.Adapt<UserDetailsDto>();
                _unitOfWork.UserDetails.InsertUserDetails(userDetailsDto);
                _unitOfWork.Save();
                return Ok("UserDetails added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to add UserDetails: {ex.Message}");
            }
        }

        [HttpPut("update-user-details")]
        public ActionResult UpdateUserDetails([FromForm] UserDetailsCreateOrUpdateDto userDetails)
        {
            try
            {
                var userDetailsDto = userDetails.Adapt<UserDetailsDto>();

                if (userDetails.formFile != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        userDetails.formFile.CopyToAsync(memoryStream);
                        var imageBytes = memoryStream.ToArray();

                        var uploadDirectory = Path.Combine(_hostingEnvironment.WebRootPath, "UploadedImages");
                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(userDetails.formFile.FileName);
                        var imagePath = Path.Combine(uploadDirectory, fileName);

                        userDetailsDto.ImageUrl = imageBytes;
                        userDetailsDto.ImageLocalPath = uploadDirectory;

                        System.IO.File.WriteAllBytesAsync(imagePath, imageBytes);
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

                var existingUser = _unitOfWork.UserDetails.GetUserDetails(userDetailsDto.Email);
                _userDbContext.Entry(existingUser).State = EntityState.Detached;
                userDetailsDto.CreationDate = existingUser.CreationDate;
                userDetailsDto.UpdationDate = DateTime.Now;
                _unitOfWork.UserDetails.UpdateUserDetails(userDetailsDto);
                _unitOfWork.Save();
                return Ok("UserDetails updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to update UserDetails: {ex.Message}");
            }
        }

        [HttpDelete("delete-user-details/{email}")]
        public ActionResult DeleteUserDetails(string email)
        {
            try
            {
                var userDetailsDto = _unitOfWork.UserDetails.GetUserDetails(email);
                if (userDetailsDto == null)
                {
                    return NotFound($"UserDetails with email '{email}' not found.");
                }

                var userDetails = userDetailsDto.Adapt<Models.Models.UserDetails>(); // Adapt to entity

                _unitOfWork.UserDetails.DeleteUserDetails(userDetails);
                _unitOfWork.Save();
                return Ok("UserDetails deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to delete UserDetails: {ex.Message}");
            }
        }

    }
}
