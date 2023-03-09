using System.ComponentModel.DataAnnotations;

namespace Domain.Services.Storages
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
        public string OpeningHour { get; set; }
        [Required]
        public string ClosingHour { get; set; }
        [Required]
        public List<DayOfWeek> DayOfWeeks { get; set; }
    }
}
