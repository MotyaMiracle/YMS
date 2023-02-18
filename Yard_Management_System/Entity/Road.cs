using Microsoft.EntityFrameworkCore;
namespace Yard_Management_System.Entity
{
    public class Road
    {
        public Guid Id { get; set; }
        public Guid StorageId { get; set; }
        public Guid DriverId { get; set; }

        /// <summary>
        /// Год, месяц, день, час, минута
        /// </summary>
        public DateTime ArrivalTime { get; set; }
        public Status NowStatus { get; set; }
        public enum Status { Create, Arrived, InArchive }
    }
}
