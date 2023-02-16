using System.ComponentModel.DataAnnotations;

namespace Yard_Management_System.Entity
{
    /// <summary>
    /// Пользователь для регистрации
    /// </summary>
    public class RegisterNewUser
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }

    }
}
