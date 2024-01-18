using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sugamta.API.Repository.Interface;

namespace Sugamta.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgencyController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public AgencyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork=unitOfWork;
        }

        [HttpGet("get-agency-all")]
        public IActionResult GetAgency()
        {
            try
            {
                var result = _unitOfWork.Agency.GetAgencies();
                if (result == null)
                {
                    return BadRequest("Agency Data Not Found");
                }
                return Ok(result);
            }  
            
             catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet("get-agency")]

        public IActionResult GetAgencyByEmail(string email)
        {
            try
            {
                var result = _unitOfWork.Agency.GetAgencyByEmail(email);
                if (result == null)
                {
                    return NotFound("Ageny not Found using This Email");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
