using Domain.Enums;

namespace Domain.Entity
{
    public class Timeslot
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public GateStatus Status { get; set; }
    }
}
