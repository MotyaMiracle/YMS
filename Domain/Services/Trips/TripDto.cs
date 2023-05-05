using Domain.Enums;
using Domain.Shared;

namespace Domain.Services.Trips
{
    public class TripDto
    {
        public string Id { get; set; }
        public LookUpDto StorageId { get; set; }
        public LookUpDto DriverId { get; set; }
        public LookUpDto TruckId { get; set; }
        public LookUpDto TrailerId { get; set; }
        public LookUpDto GateId { get; set; }
        public DateTime ArrivalTime { get; set; }
        public TripStatus NowStatus { get; set; }
        public string Number { get; set; }
        public int PalletsCount { get; set; }
        public string Backlights { get; set; }
    }
}
