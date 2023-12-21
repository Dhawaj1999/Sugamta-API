using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Models.Models
{
    public class UserDetails
    {
        [Key]
        [ForeignKey("User")]
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
        [Required]  
        public string Address { get; set; }
        public string City { get; set; }
        [Required]  
        public string State { get; set; }
        [Required]
        public string Country { get; set; }

        [Required(ErrorMessage = "PhoneNumber is required")]
        [RegularExpression(@"^\+\d{1,3}(\d{10})$", ErrorMessage = "Invalid phone number format")]
        public string PhoneNumber { get; set; }

        [RegularExpression(@"^\+\d{1,3}(\d{10})$", ErrorMessage = "Invalid alternate phone number format")]
        public string AlternatePhoneNumber { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        [Required]
        public DateTime UpdationDate { get; set;}
        [JsonIgnore]
        [ValidateNever]
        public  User User { get; set; }
    }
}
