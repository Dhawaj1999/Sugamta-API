
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Sugamta.API.DTOs.UserDTOs;
using Sugamta.API.Repository.Interface;

namespace Sugamta.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;

        public UserController(IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public ActionResult GetUsers()
        {
            try
            {
                var users = _unitOfWork.user.GetUsers();
                var userDTOs = users.Adapt<List<UserDto>>(); // Using Mapster for mapping
                return Ok(userDTOs);
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet("{email}")]
        public ActionResult GetUser(string email)
        {
            try
            {
                var user = _unitOfWork.user.GetUser(email);

                if (user == null)
                {
                    return NotFound($"User with email '{email}' not found.");
                }

                var userDTO = user.Adapt<UserDto>(); // Using Mapster for mapping

                // Do not return the hashed password to the client
                userDTO.Password = null;

                return Ok(userDTO);
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPost]
        public ActionResult<UserDto> CreateUser(UserDto userDto)
        {
            try
            {
                // Hash the password before storing it
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
                userDto.Password = hashedPassword;

                _unitOfWork.user.CreateUser(userDto); // Using Mapster for mapping

                // Clear the password before returning the user object
                userDto.Password = null;

                _unitOfWork.Save();

                return CreatedAtAction(nameof(GetUser), new { email = userDto.Email }, userDto);
            }
            catch (Exception ex)
            {
                
                return BadRequest($"Failed to create user: {ex.Message}");
            }
        }



        [HttpPatch("{email}")]
        public IActionResult UpdateUser(int email, [FromBody] UserDto updatedUserDto)
        {
            try
            {
                // Call the private method to update the user by ID
                UpdateUserByEmail(email, updatedUserDto);
              
                return Ok("Update Successful"); // Adjust the response message as needed
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                return StatusCode(500, $"Error updating user: {ex.Message}");
            }
        }

        
        private void UpdateUserByEmail(int email, UserDto updatedUserDto)
        {
            _unitOfWork.user.UpdateUser(email, updatedUserDto);
            _unitOfWork.Save();


        }








        [HttpDelete("{email}")]
        public  IActionResult DeleteUser(string email)
        {
            try
            {
                if (!_unitOfWork.user.DeleteUser(email))
                {
                    return Ok("Invalid user Email or User not found"); // changes here
                }

                return Ok("Delete Successfully"); // changes here
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                return StatusCode(500, $"Error deleting user: {ex.Message}"); // changes here
            }
        }

     
 
       


    }
}
