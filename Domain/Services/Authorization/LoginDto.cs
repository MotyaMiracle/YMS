using System.ComponentModel.DataAnnotations;
namespace Domain.Services.Authorization
{
    public class LoginDto
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
