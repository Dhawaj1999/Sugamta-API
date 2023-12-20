using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Models
{

    public class User
    {
        
        public int UserID { get; set; }
        [Key]

        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
        [NotMapped]
        public string Image { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        
        public DateTime UpdationDate { get; set; }
         


    }
}



