using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models.DTOs.UserDetailsDTOs
{
    public class UserDetailsCreateOrUpdateDto
    {
        public string Email { get; set; }
        public byte[]? ImageUrl { get; set; }
        public IFormFile? formFile { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
        public string AlternatePhoneNumber { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdationDate { get; set; }
    }
}
