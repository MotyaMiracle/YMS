using System.ComponentModel.DataAnnotations;

namespace Yard_Management_System.CRUDs.Storage
{
    public class DeleteStorage
    {
        [Required]
        public string Name { get; set; }
    }
}
