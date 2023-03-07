using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Domain.Services.Companies
{
    public class CompanyDto
    {
        public string Id { get; set; }

        /// <summary>
        /// Название компании
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Id Грузовика
        /// </summary>
        public Guid TruckId { get; set; }

        /// <summary>
        /// Id Прицепа
        /// </summary>
        public Guid TrailerId { get; set; }
    }
}
