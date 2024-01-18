using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models.DTOs.UserDTOs
{
    public class UserUpdateDto
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int IsDeleted { get; set; } = 0;
        public int RoleId { get; set; }
    }
}
