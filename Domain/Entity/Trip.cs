using Domain.Enums;
using Domain.Services.ColorStatus;

namespace Domain.Entity

{
    public class Trip:IListDto
    {
        public Guid Id { get; set; }
        public Guid? StorageId { get; set; }
        public Guid DriverId { get; set; }
        public Guid TruckId { get; set; }
        public Guid TrailerId { get; set; }
        public Guid CompanyId { get; set; }
        public Guid? GateId { get; set; }
        public Guid? TimeslotId { get; set; }

        /// <summary>
        /// Планируемое время прибытия
        /// </summary>
        public DateTime ArrivalTimePlan { get; set; }

        /// <summary>
        /// Фактическое время прибытия
        /// </summary>
        public DateTime? ArrivalTimeFact { get; set; }
        public TripStatus NowStatus { get; set; }
        public Driver Driver { get; set; }
        public Storage Storage { get; set; }
        public Truck Truck { get; set; }
        public Trailer Trailer { get; set; }
        public Timeslot Timeslot { get; set; }
        public Gate Gate { get; set; }
        public Company Company { get; set; }
        public string Number { get; set; }
        public int PalletsCount { get; set; }
        public string Backlights { get; set; }
        public byte[] QRCode { get; set; }
    }
}
