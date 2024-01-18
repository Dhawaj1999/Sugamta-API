using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Text.Json.Serialization;

namespace Models.Models
{
    public class SecondaryClient
    {
        
        [Key]
        public string SecondaryClientEmail { get; set; }

        public string? Name { get; set; }
      
        public string Password { get; set; }
        
        public DateTime? CreationDate { get; set; }
        public int? RoleId { get; set; }
        [ForeignKey("RoleId")]
        [JsonIgnore]
        [ValidateNever]
        public Role Roles { get; set; }
        public int? IsDeleted { get; set; } = 0;
        public string? OTP { get; set; }
      
        [ValidateNever]
        [JsonIgnore]
        public PrimaryClient PrimaryClient { get; set; }
       // public int IsDeleted { get; set; } = 0;
        [ForeignKey("PrimaryClient")]
        public string PrimaryClientEmail { get; set; }
    }
}
