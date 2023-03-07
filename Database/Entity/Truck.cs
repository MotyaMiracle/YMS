namespace Database.Entity
{
    public class Truck
    {
        public Guid Id { get; set; }
        public string CarBrand { get; set; }
        public string CarNumber { get; set; }
        public Guid CompanyId { get; set; }
        public Company Company { get; set; }
        public string Description { get; set; }
    }
}
