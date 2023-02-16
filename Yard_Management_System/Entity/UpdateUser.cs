using System.ComponentModel.DataAnnotations;

namespace Yard_Management_System.Entity
{
    public class UpdateUser
    {
        [Required]
        public string Login { get; set; }
        public string NewLogin { get; set; }
        public string NewPhoneNumber { get; set; }
        public string NewEmail { get; set; }
        public string NewPassword { get; set; }
    }
}
