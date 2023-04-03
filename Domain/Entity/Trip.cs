using Domain.Enums;

namespace Domain.Entity

{
    public class Trip
    {
        public Guid Id { get; set; }
        public Guid StorageId { get; set; }
        public Guid DriverId { get; set; }
        public Guid TruckId { get; set; }
        public Guid TrailerId { get; set; }
        public Guid GateId { get; set; }
        public Guid TimeslotId { get; set; }

        /// <summary>
        /// Год, месяц, день, час, минута
        /// </summary>
        public DateTime ArrivalTime { get; set; }
        public TripStatus NowStatus { get; set; }
        public Driver Driver { get; set; }
        public Storage Storage { get; set; }
        public Truck Truck { get; set; }
        public Trailer Trailer { get; set; }
        public Timeslot Timeslot { get; set; }
        public Gate Gate { get; set; }
        public string Number { get; set; }
        public int PalletsCount { get; set; }
    }
}
