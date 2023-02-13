using System.ComponentModel.DataAnnotations;

namespace Yard_Management_System.Models
{
    public class LoginDto
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
