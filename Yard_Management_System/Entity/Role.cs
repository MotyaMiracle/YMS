using Microsoft.EntityFrameworkCore;
using System.Security;

namespace Yard_Management_System.Entity
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Permissions> ListOfPermissions { get; set; }
    }
}
