namespace Database.Entity
{
    public class Gate
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Height { get; set; }
        public int PalletHandlingTime { get; set; }
        public GateStatus Status { get; set; }
        public enum GateStatus { Loading, Unloading}
        public string OpeningHour { get; set; }
        public string ClosingHour { get; set; }
        public Guid StorageId { get; set; }
    }
}
