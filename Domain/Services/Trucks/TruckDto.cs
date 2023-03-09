using Database.Entity;
using Domain.Shared;

namespace Domain.Services.Trucks
{
    public class TruckDto
    {
        public string Id { get; set; }
        public string Brand { get; set; }
        public string Number { get; set; }
        public LookUpDto CompanyId { get; set; }
        public string Description { get; set; }
    }
}
