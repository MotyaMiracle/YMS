namespace Yard_Management_System.Models
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public User? User { get; set; }
    }
}