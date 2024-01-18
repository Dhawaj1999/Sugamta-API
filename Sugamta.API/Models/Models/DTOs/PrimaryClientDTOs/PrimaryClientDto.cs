using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models.Models.DTOs.PrimaryClientDTOs
{
    public class PrimaryClientDto
    {
        public string PrimaryClientEmail { get; set; }
        public string PrimaryClientName { get; set; }
        public string Password { get; set; }
        public DateTime CreationDate { get; set; }
        public int RoleId { get; set; }

        public string AgencyEmail { get; set; }
        public string? OTP { get; set; }
        public DateTime OTPGeneratedDate { get; set; }
        public int IsDeleted { get; set; } = 0;
        public string AgencyName {  get; set; }
    }
}
