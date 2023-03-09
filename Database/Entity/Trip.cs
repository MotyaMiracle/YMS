using Database.Entity;
namespace Yard_Management_System.Entity
{
    public class Trip
    {
        public Guid Id { get; set; }
        public Guid StorageId { get; set; }
        public Guid DriverId { get; set; }
        public Guid TruckId { get; set; }
        public Guid TrailerId { get; set; }

        /// <summary>
        /// Год, месяц, день, час, минута
        /// </summary>
        public DateTime ArrivalTime { get; set; }
        public Status NowStatus { get; set; }
        public enum Status { Create, Arrived, InArchive }
        public Driver Driver { get; set; }
        public Storage Storage { get; set; }
        public Truck Truck { get; set; }
        public string Number { get; set; }
    }
}
