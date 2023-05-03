using Domain.Enums;

namespace Domain.Entity
{
    public class Timeslot
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public Guid TripId { get; set; }
        public Trip Trip { get; set; }
        public OperationType Status { get; set; }
        public int Minutes => (DateTime.Parse(To) - DateTime.Parse(From)).Minutes;

    }
}
