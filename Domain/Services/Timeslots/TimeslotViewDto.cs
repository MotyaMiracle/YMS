﻿namespace Domain.Services.Timeslots
{
    public class TimeslotViewDto
    {
        public List<string> AllGates { get; set; }
        public DateTime Date { get; set; }
        public string From { get; set; }
        public string To { get; set; }
    }
}
