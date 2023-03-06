using Domain.Shared;

namespace Domain.Services.Users
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public string PhoneNumber { get; set; }
        public LookUpDto RoleId { get; set; }
    }
}
