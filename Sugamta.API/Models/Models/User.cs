using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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

    }
}



