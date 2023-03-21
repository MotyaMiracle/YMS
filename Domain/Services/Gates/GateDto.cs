using Domain.Enums;
using Domain.Shared;

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
        public int PalletHandlingTime { get; set; }

        /// <summary>
        /// Тип выполняемых операций
        /// </summary>
        public OperationType Status { get; set; }

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
        public LookUpDto StorageId { get; set; }
    }
}
