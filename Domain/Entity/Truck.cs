using Domain.Enums;

namespace Domain.Entity
{
    public class Truck
    {
        public Guid Id { get; set; }
        public string Brand { get; set; }
        public string Number { get; set; }
        public Guid CompanyId { get; set; }
        public Company Company { get; set; }
        public ColorStatus? ColorStatus { get; set; }
        public Backlights? Backlight { get; set; }
        public string Description { get; set; }
    }
}
