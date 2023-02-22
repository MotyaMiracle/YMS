using System.ComponentModel.DataAnnotations;

namespace Domain.Services.User
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public Guid RoleId { get; set; }
    }
}
