using Mapster;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Sugamta.API.DTOs.UserDetailsDTOs;
using Sugamta.API.Repository.Interface;
using System;

namespace Sugamta.API.Controllers
{
    [ApiController]
    [Route("api")]
    public class UserDetailsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserDetailsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("get-user-details/{email}")]
        public ActionResult<UserDetailsDto> GetUserDetails(string email)
        {
            try
            {
                var userDetails = _unitOfWork.UserDetails.GetUserDetails(email);
                if (userDetails == null)
                {
                    return NotFound($"UserDetails with email '{email}' not found.");
                }

                var userDetailsDto = userDetails.Adapt<UserDetailsDto>();
                return Ok(userDetailsDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to retrieve UserDetails: {ex.Message}");
            }
        }

        [HttpPost("add-user-details")]
        public ActionResult AddUserDetails([FromBody] UserDetailsDto userDetailsDto)
        {
            try
            {
                var existingUser = _unitOfWork.UserDetails.GetUserDetails(userDetailsDto.Email);
                if (existingUser != null)
                {
                    return BadRequest("This UserDetails already added. Please go for updating UserDetails.");
                }

                var userDetails = userDetailsDto.Adapt<UserDetailsDto>();
                _unitOfWork.UserDetails.InsertUserDetails(userDetails);
                _unitOfWork.Save();
                return Ok("UserDetails added successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to add UserDetails: {ex.Message}");
            }
        }

        [HttpPut("update-user-details/{email}")]
        public ActionResult UpdateUserDetails(string email, [FromBody] UserDetailsDto userDetailsDto)
        {
            try
            {
                if (email != userDetailsDto.Email)
                {
                    return BadRequest("UserEmail in the URL does not match UserEmail in the request body.");
                }

                var userDetails = userDetailsDto.Adapt<UserDetailsDto>();
                _unitOfWork.UserDetails.UpdateUserDetails(userDetails);
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
