using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AutoMapper.Configuration.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Models.Models
{
    public class UserDetails
    {
        [Key]
        [ForeignKey("User")]
        public string Email { get; set; }
		public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
		public string AlternatePhoneNumber { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdationDate { get; set;}
        public byte[]? ImageUrl { get; set; }
        public string? ImageLocalPath { get; set; }
        [JsonIgnore]
        [ValidateNever]
        public  User User { get; set; }
    }
}
