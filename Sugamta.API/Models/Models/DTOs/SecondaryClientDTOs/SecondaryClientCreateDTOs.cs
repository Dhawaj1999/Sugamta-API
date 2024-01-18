using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Models.Models.DTOs.SecondaryClientDTOs
{
    public class SecondaryClientCreateDTOs
    {

        public string SecondaryClientEmail { get; set; }

        public string Name { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public string PrimaryClientEmail { get; set; }

       
    }
}
