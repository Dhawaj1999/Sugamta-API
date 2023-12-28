using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models.Models
{

    public class User
    {
      [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        public int UserID { get; set; }
        [Key] 
        public string Email { get; set; }
        public string Name { get; set; }
       
        public string Password { get; set; }
       
        public DateTime CreationDate { get; set; }
        public int IsDeleted { get; set; } = 0;
        public string CreatedBy { get; set; }// changes here, Add new column 
        [JsonIgnore]
        [ValidateNever]
        public UserDetails UserDetails { get; set; }

    }
}



