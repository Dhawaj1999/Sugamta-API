using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Models.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Sugamta.API.DTOs.UserDetailsDTOs
{
    public class UserDetailsDto
    {
        [Key]
        [ForeignKey("User")]
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int StateId { get; set; }
        public int CountryId { get; set; }
        public string PhoneNumber { get; set; }
        public string AlternatePhoneNumber { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdationDate { get; set; }
        public byte[]? ImageUrl { get; set; }
        public string? ImageLocalPath { get; set; }
        public IFormFile? IFormFile { get; set; }
        [JsonIgnore]
        [ValidateNever]
        public User User { get; set; }
        [JsonIgnore]
        [ValidateNever]
        public Country Country { get; set; }
        [JsonIgnore]
        [ValidateNever]
        public State State { get; set; }
    }
}
