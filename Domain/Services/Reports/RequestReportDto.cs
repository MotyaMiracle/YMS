using Domain.Enums;

namespace Domain.Services.Reports
{
    public class RequestReportDto
    {
        /// <summary>
        /// Дата начала периода
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Дата окончания периода
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Тип фильтрации
        /// </summary>
        public FilterDetalization FilterDetalization { get; set; }
        public bool DetailByCompany { get; set; }
        public bool DetailByTrip { get; set; }

    }
}
