using System.ComponentModel.DataAnnotations;

namespace Yard_Management_System.CRUDs.Storage
{
    public class CreateStorage
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public double[] Coordinates { get; set; }
        [Required]
        public string OpeningHours { get; set; }
        [Required]
        public List<DayOfWeek> DayOfWeeks { get; set; }
    }
}
