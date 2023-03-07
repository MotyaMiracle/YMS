namespace Domain.Services.Gates
{
    public class GateDto
    {
        public string Id { get; set; }

        /// <summary>
        /// Номер ворот
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Высота
        /// </summary>
        public string Height { get; set; }

        /// <summary>
        /// Время обработки палет
        /// </summary>
        public string PalletHandlingTime { get; set; }

        /// <summary>
        /// Тип выполняемых операций
        /// </summary>
        public StatusOfGates Status { get; set; }
        public enum StatusOfGates { Loading, Unloading }

        /// <summary>
        /// Время открытия
        /// </summary>
        public string OpeningHour { get; set; }

        /// <summary>
        /// Время закрытия
        /// </summary>
        public string ClosingHour { get; set; }

        /// <summary>
        /// Код склада
        /// </summary>
        public Guid StorageId { get; set; }
    }
}
