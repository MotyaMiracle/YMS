using System.IO;

namespace Domain.Entity
{
    public class EntityFile
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public Guid EntityId { get; set; }
        public byte[] Data  { get; set; }
    }
}
