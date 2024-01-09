using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models.DTOs.UserDTOs
{
    public class UserOtpDto
    {
        public string Email { get; set; }
        public string? OTP { get; set; }
    }
}
