using Domain.Enums;

namespace Domain.Entity
{
    public class Gate
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Height { get; set; }
        public int PalletHandlingTime { get; set; }
        public OperationType Status { get; set; }
        public string OpeningHour { get; set; }
        public string ClosingHour { get; set; }
        public Guid StorageId { get; set; }
    }
}
