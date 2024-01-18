using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models.Models.DTOs.SecondaryClientDTOs
{
    public class SecondaryClientDto
    {
        
        public string SecondaryClientEmail { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public DateTime CreationDate { get; set; }
        public int RoleId { get; set; }
        [ForeignKey("RoleId")]
        [JsonIgnore]
        [ValidateNever]
        public Role Roles { get; set; }
        public string OTP { get; set; }
        public int IsDeleted { get; set; } = 0;
        [ValidateNever]
        [JsonIgnore]
        public PrimaryClient PrimaryClient { get; set; }
        // public int IsDeleted { get; set; } = 0;
        [ForeignKey("PrimaryClient")]
        public string PrimaryClientEmail { get; set; }

        public string RoleType { get; set; }
    }
}
