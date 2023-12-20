
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Sugamta.API.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using BCrypt.Net; // Add this namespace for password hashing
using Microsoft.Extensions.Configuration;

namespace Sugamta.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        //private readonly IUser _userRepo;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        public UserController( IConfiguration configuration,IUnitOfWork unitOfWork)
        {
           // _userRepo = userRepo;
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public ActionResult<List<User>> GetUsers()
        {
            try
            {
                var users = _unitOfWork.user.GetUsers().ToList();
                return Ok(users);
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet("{email}")]
        public ActionResult<User> GetUser(string email)
        {
            try
            {
                var user = _unitOfWork.user.GetUser(email);

                if (user == null)
                {
                    return NotFound($"User with email '{email}' not found.");
                }

                // Do not return the hashed password to the client
                user.Password = null;

                return Ok(user);
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPost]
        public ActionResult<User> CreateUser(User user)
        {
            try
            {
                // Hash the password before storing it
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);
                user.Password = hashedPassword;

                _unitOfWork.user.CreateUser(user);
                // Clear the password before returning the user object
                user.Password = null;

                return CreatedAtAction(nameof(GetUser), new { email = user.Email }, user);
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPut("{email}")]
        public IActionResult UpdateUser(string email, User user)
        {
            try
            {
                // Hash the new password before updating it
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);
                user.Password = hashedPassword;

                if (!_unitOfWork.user.UpdateUser(email, user))
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpDelete("{email}")]
        public IActionResult DeleteUser(string email)
        {
            try
            {
                if (!_unitOfWork.user.DeleteUser(email))
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}















