using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models.Models
{
    public class User
    {
        [JsonIgnore]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }
        [Key]
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public DateTime CreationDate { get; set; }
        [JsonIgnore]
        [ValidateNever]
        public UserDetails UserDetails { get; set; }
        public int IsDeleted { get; set; } = 0;
        [Required]
        public int RoleId { get; set; }
        [ForeignKey("RoleId")]
        [JsonIgnore]
        [ValidateNever]
        public Role Roles { get; set; }
        [ValidateNever] 
        public string? OTP { get; set; }
    }
}
