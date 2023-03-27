namespace Domain.Services.Timeslots
{
    public class TimeslotViewDto
    {
        public string GateName { get; set; }
        public DateTime Date { get; set; }
        public string From { get; set; }
        public string To { get; set; }
    }
}
