namespace Database.Entity
{
    public class Gate
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Height { get; set; }
        public TimeOnly PalletHandlingTime { get; set; }
        public StatusOfGates Status { get; set; }
        public enum StatusOfGates { Loading, Unloading}
        public TimeOnly OpeningHour { get; set; }
        public TimeOnly ClosingHour { get; set; }
        public Guid StorageId { get; set; }
    }
}
