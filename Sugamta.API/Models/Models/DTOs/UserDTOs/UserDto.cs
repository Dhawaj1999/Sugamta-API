

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Models.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Sugamta.API.DTOs.UserDTOs
{
    public class UserDto
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        public int UserID { get; set; }
        [Key]
        public string Email { get; set; }
        public string Name { get; set; }

        public string Password { get; set; }

        public DateTime CreationDate { get; set; }

        public string CreatedBy { get; set; }// changes here, Add new column 
        public int IsDeleted { get; set; } = 0;
        [JsonIgnore]
        [ValidateNever]
        public UserDetails UserDetails { get; set; }

    }
}
