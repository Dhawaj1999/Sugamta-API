using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models.Models
{
    public class UserLoginHistory
    {
        [Key]
        [JsonIgnore]
        public int LoginHistoryId { get; set; }
        public string Email { get; set; }
        [ForeignKey("Email")]
        [JsonIgnore]
        [ValidateNever]
        public User User { get; set; }
        public DateTime LastLoginTime { get; set; }
        public int RoleId { get; set; }
        public string RoleType { get; set; }
    }
}
