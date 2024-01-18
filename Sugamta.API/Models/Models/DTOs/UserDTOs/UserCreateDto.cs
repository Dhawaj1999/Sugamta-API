using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models.Models.DTOs.UserDTOs
{
    public class UserCreateDto
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public DateTime CreationDate { get; set; }
        public int IsDeleted { get; set; } = 0;
        public int RoleId { get; set; }
        public string? OTP { get; set; }
    }
}
