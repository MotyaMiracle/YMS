﻿using System.ComponentModel.DataAnnotations;

namespace Yard_Management_System.Services.Storages
{
    public class StorageDto
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public double Latitude { get; set; }
        [Required]
        public double Longitude { get; set; }
        [Required]
        public string OpeningHours { get; set; }
        [Required]
        public List<DayOfWeek> DayOfWeeks { get; set; }
    }
}
