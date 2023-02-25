using System.IO;
namespace Yard_Management_System.Entity
{
    public class EntityFile
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public Guid DriverId { get; set; }
        public byte[] Data  { get; set; }
    }
}
