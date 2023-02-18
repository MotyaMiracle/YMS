using System.ComponentModel.DataAnnotations;

namespace Yard_Management_System.CRUDs.Storage
{
    public class UpdateStorage
    {
        [Required]
        public string OldName { get; set; }
        public string NewName { get; set; }
        public string NewAddress { get; set; }
        public double[] NewCoordinates { get; set; }
        public string NewOpeningHours { get; set; }
        public List<DayOfWeek> NewDayOfWeeks { get; set; }
    }
}
