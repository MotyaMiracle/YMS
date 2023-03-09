using Database.Entity;
using Domain.Shared;
using System.ComponentModel.DataAnnotations;
using Yard_Management_System.Entity;

namespace Domain.Services.Trips
{
    public class TripDto
    {
        public string Id { get; set; }
        public LookUpDto StorageId { get; set; }
        public LookUpDto DriverId { get; set; }
        public LookUpDto TruckId { get; set; }
        public DateTime ArrivalTime { get; set; }
        public Status NowStatus { get; set; }
        public enum Status { Create, Arrived, InArchive }
        public string Number { get; set; }
    }
}
