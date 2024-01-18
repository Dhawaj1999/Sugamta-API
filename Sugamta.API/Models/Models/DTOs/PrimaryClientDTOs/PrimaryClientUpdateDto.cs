using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models.DTOs.PrimaryClientDTOs
{
    public class PrimaryClientUpdateDto
    {
        public string PrimaryClientEmail { get; set; }
        public string PrimaryClientName { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public int IsDeleted { get; set; } = 0;
    }
}
