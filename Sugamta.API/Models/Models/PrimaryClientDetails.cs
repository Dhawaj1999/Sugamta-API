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
    public class PrimaryClientDetails
    {
        [Key]
        [ForeignKey("PrimaryClient")]
        public string PrimaryClientEmail { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int StateId { get; set; }
        public int CountryId { get; set; }
        public string PhoneNumber { get; set; }
        public string AlternatePhoneNumber { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdationDate { get; set; }
        public byte[]? ImageUrl { get; set; }
        [JsonIgnore]
        [ValidateNever]
        public Country Country { get; set; }
        [JsonIgnore]
        [ValidateNever]
        public State State { get; set; }
        [JsonIgnore]
        [ValidateNever]
        public PrimaryClient PrimaryClient { get; set; }

    }
}
