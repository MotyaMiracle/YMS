﻿using Domain.Enums;
using System.Security.Cryptography;

namespace Domain.Entity
{
    public class Timeslot
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public GateStatus Status { get; set; }
        public Guid TripId { get; set; }
        public Trip Trip { get; set; }
    }
}