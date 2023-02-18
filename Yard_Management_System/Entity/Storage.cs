using Microsoft.EntityFrameworkCore;
namespace Yard_Management_System.Entity
{
    public class Storage
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double[] Coordinates { get; set; }
        /// <summary>
        /// Часы работы
        /// </summary>
        public string OpeningHours { get; set; }
        /// <summary>
        /// Enum 0 - Воскресенье, 5 - Пятница
        /// </summary>
        public List<DayOfWeek> DayOfWeeks { get; set; }
    }
}
