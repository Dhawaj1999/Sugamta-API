using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Serilog;
using Sugamta.API.Repository.Interface;
using System.ComponentModel.DataAnnotations;

namespace Sugamta.API.Controllers
{
    [Route("api/LoginHistory")]
    [ApiController]
    public class UserLoginHistoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserLoginHistoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("getalluserloginhistory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<UserLoginHistory>> GetAllUserLoginHistory()
        {
            try
            {
                IEnumerable<UserLoginHistory> userLoginHistories = _unitOfWork.UserLoginHistory.GetAllLoginHistory();
                if (userLoginHistories == null || userLoginHistories.Any() == false)
                {
                    Log.Information("No Data is available in User Login History Table in Database.");
                    ModelState.AddModelError("Empty Data", "No Login History Found.");
                    return NotFound(ModelState);
                }

                Log.Information("User Login History: ");
                foreach(var item in userLoginHistories)
                {
                    Log.Information("User Id: " + item.LoginHistoryId + " , User Email: " + item.Email + " , Last Login Time: " + item.LastLoginTime + " , Last Logout Time: " + item.LastLogoutTime);
                }
                
                return Ok(userLoginHistories);
            } catch (Exception ex)
            {
                Log.Information($"{ex.Message}");
                return BadRequest(ex.Message);
            }        
        }

        [HttpGet("getuserloginhistorybyemail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<UserLoginHistory>> GetUserLoginHistoryByEmail([EmailAddress]string email)
        {
            try
            {
                IEnumerable<UserLoginHistory> userLogins = _unitOfWork.UserLoginHistory.GetLoginHistoryByEmail(e => e.Email == email).ToList();
                if (userLogins == null || userLogins.Any() == false)
                {
                    ModelState.AddModelError("Empty", "No Login History found for " + email);
                    return NotFound(ModelState);
                }
                return Ok(userLogins);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("createloginhistory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult CreateUserLoginHistory([FromBody] UserLoginHistory userLoginHistory)
        {
            try
            {
                if (userLoginHistory == null)
                {
                    return BadRequest();
                } else if (userLoginHistory.Email == null)
                {
                    ModelState.AddModelError("Email Error", "Email cannot be blank.");
                    return BadRequest(ModelState);
                }

                var user = _unitOfWork.user.GetUser(userLoginHistory.Email);
                if (user == null)
                {
                    Log.Information("There is no user with mail as " + userLoginHistory.Email + " in User Table in Database. Create a user first with same email to create user login history record.");
                    ModelState.AddModelError("No User Error", "User with the email " +  userLoginHistory.Email + " is not found.");
                    return NotFound(ModelState);
                }

                _unitOfWork.UserLoginHistory.CreateLoginHistory(userLoginHistory);
                _unitOfWork.Save();
                return Ok("User Login History is Created Successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpPut("updateuserloginhistory")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public ActionResult UpdateUserLoginHistory([FromBody] UserLoginHistory userLoginHistory)
        //{
        //    try
        //    {
        //        if(userLoginHistory == null)
        //        {
        //            ModelState.AddModelError("Null Error", "Please enter user details to update login history.");
        //            return BadRequest(ModelState);
        //        } else if (userLoginHistory.Email == null)
        //        {
        //            ModelState.AddModelError("Email Null Error", "Please enter user email to update login history.");
        //            return BadRequest(ModelState);
        //        }

        //        var user = _unitOfWork.user.GetUser(userLoginHistory.Email);
        //        if (user == null)
        //        {
        //            ModelState.AddModelError("No User Error", "User with the email " + userLoginHistory.Email + " is not found.");
        //            return NotFound(ModelState);
        //        }


        //        _unitOfWork.UserLoginHistory.UpdateLoginHistory(userLoginHistory);
        //        _unitOfWork.Save();
        //        return Ok("User Login History is Updated Successfully.");
        //    } catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        //[HttpDelete("removeuserloginhistory")]
    }
}
