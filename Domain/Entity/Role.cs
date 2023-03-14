using Domain.Enums;

namespace Domain.Entity
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Permissions> ListOfPermissions { get; set; }

    }
}
