using Database.Entity;
using Domain.Shared;

namespace Domain.Services.Trailers
{
    public class TrailerDto
    {
        public string Id { get; set; }
        public string Number { get; set; }
        public string Description { get; set; }
        public LookUpDto CompanyId { get; set; }

        /// <summary>
        /// Грузоподьемность
        /// </summary>
        public string CargoCapacity { get; set; }
    }
}
