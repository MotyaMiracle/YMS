using Database.Entity;
using System.ComponentModel.DataAnnotations;
using Yard_Management_System.Entity;

namespace Domain.Services.Trips
{
    public class TripDto
    {
        public Guid Id { get; set; }
        public Guid StorageId { get; set; }
        public Guid DriverId { get; set; }
        public DateTime ArrivalTime { get; set; }
        public Status NowStatus { get; set; }
        public enum Status { Create, Arrived, InArchive }
        public string Number { get; set; }
    }
}
