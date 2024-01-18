using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models.DTOs.UserLoginHistoryDTOs
{
    public class LinkGenerationDto
    {
        public string RegistrationLink { get; set; }
        public DateTime LinkGenerationDate { get; set; }
        public int IsActive { get; set; } = 1;
    }
}
