using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models.Models.DTOs.UserLoginHistoryDTOs
{
    public class CreateUserLoginHistoryDto
    {
        [JsonIgnore]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int CreateUserLoginHistoryId { get; set; }
        public string Email { get; set; }
        public DateTime LastLoginTime { get; set; }
        public int RoleId { get; set; }
        public string RoleType { get; set; }
    }
}
