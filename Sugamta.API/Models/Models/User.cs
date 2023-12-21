using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models.Models
{

    public class User
    {
        
        public int UserID { get; set; }
        [Key]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
        [NotMapped]
        public string Image { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        public DateTime UpdationDate { get; set; }
        [JsonIgnore]
        [ValidateNever]
        public ICollection<UserLoginHistory> LoginHistory { get; set; }
    }
}



