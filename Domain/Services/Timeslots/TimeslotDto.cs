﻿using Domain.Enums;

namespace Domain.Services.Timeslots
{
    public class TimeslotDto
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public OperationType Status { get; set; }
    }
}
