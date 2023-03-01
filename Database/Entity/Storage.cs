using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yard_Management_System.Entity;

namespace Database.Entity
{
    public class Storage
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        /// <summary>
        /// Широта
        /// </summary>
        public double Latitude { get; set; }
        /// <summary>
        /// Долгота
        /// </summary>
        public double Longitude { get; set; }
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
