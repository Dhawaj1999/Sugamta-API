using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Sugamta.API.Repository.Interface;
using System.Text.RegularExpressions;
using System;

namespace Sugamta.API.Controllers
{
   // [Route("api/[controller]")]
    [ApiController]
    public class UserDetailsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserDetailsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork=unitOfWork;
        }

        [HttpGet("api/get-user-details/{email}")]
        public ActionResult GetUserDetails(string email)
        {
            try
            {
                var data = _unitOfWork.UserDetails.GetUserDetails(email);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to retrieve UserDetails{ex.Message}");
            }
        }
        [HttpPost("api/add-user-details")]
        public ActionResult AddUserDetails(UserDetails userDetails)
         {
            try
            {

                _unitOfWork.UserDetails.InsertUserDetails(userDetails);
                _unitOfWork.Save();
                return Ok("UserDetails Added Successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to Add UserDetails : {ex.Message}");
            }
        }

        [HttpPut("api/update-user-details{email}")]
        public ActionResult UpdateUserDetails(string email, UserDetails userDetails)
        {
            try
            {
                if (email != userDetails.Email)
                {
                    return BadRequest("UserEmail in the URL does not match UserEmail in the request body.");
                }
                _unitOfWork.UserDetails.UpdateUserDetails(userDetails);
                return Ok("UserDetails Updated Successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to Update UserDetails : {ex.Message}");
            }
        }

        [HttpDelete("api/delete-user-details/{email}")]
        public ActionResult DeleteUserDetails(string email)
        {
            try
            {
                var data=_unitOfWork.UserDetails.GetUserDetails(email);
                if (data == null)
                {
                    return NotFound($"UserDetails with this Email not found.");
                }
                //  _unitOfWork.UserDetails.DeleteUserDetails(new UserDetails { Email = email });
                _unitOfWork.UserDetails.DeleteUserDetails(data);
                return Ok("UserDetails Deleted Successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Failed to Delete UserDetails: {ex.Message}");
            }
        }
    }
}
