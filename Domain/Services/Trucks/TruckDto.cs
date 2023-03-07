using Database.Entity;
using Domain.Shared;

namespace Domain.Services.Trucks
{
    public class TruckDto
    {
        public string Id { get; set; }
        public string CarBrand { get; set; }
        public string CarNumber { get; set; }
        public LookUpDto CompanyId { get; set; }
        public string Description { get; set; }
    }
}
