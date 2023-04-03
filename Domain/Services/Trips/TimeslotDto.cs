using Domain.Shared;

namespace Domain.Services.Trips
{
    public class TimeslotDto
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public LookUpDto TripId { get; set; }
    }
}