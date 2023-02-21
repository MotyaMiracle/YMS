using System.ComponentModel.DataAnnotations;

namespace Domain.Services.Trip
{
    public class TripDto
    {
        [Required]
        public Guid TripId { get; set; }
    }
}
