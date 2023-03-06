namespace Yard_Management_System.Entity
{
    public class User
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public bool IsActive { get; set; }
        public string PhoneNumber { get; set; }
        public Guid RoleId { get; set; }
        public Role Role { get; set; }
    }
}