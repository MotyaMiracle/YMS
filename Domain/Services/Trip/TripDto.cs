using System.ComponentModel.DataAnnotations;

namespace Domain.Services.Trip
{
    public class TripDto
    {
        [Required]
        public Guid TripId { get; set; }
        public Guid DriverId { get; set; }
        public Guid StorageId { get; set; }
        public DateTime ArrivalTime { get; set; }

    }
}
