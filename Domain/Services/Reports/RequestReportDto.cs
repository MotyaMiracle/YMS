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
        /// Тип операции
        /// </summary>
        public OperationType? OperationType { get; set; }

        /// <summary>
        /// Продолжительность
        /// </summary>
        public double? Duration { get; set; }

        /// <summary>
        /// Количество паллет
        /// </summary>
        public int? PalletsCount { get; set; }
        public string? StorageName { get; set; }
        public bool? CompanyName { get; set; }
        public bool? Trips { get; set; }

    }
}
