using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class LinkGeneration
    {
        [Key]
        public int LinkGenerationId { get; set; }
        [Required]
        public string RegistrationLink { get; set; }
        [Required]
        public DateTime LinkGenerationDate { get; set; }
        [Required]
        public int IsActive { get; set; } = 1;
    }
}
