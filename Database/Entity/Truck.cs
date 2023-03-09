namespace Database.Entity
{
    public class Truck
    {
        public Guid Id { get; set; }
        public string Brand { get; set; }
        public string Number { get; set; }
        public Guid CompanyId { get; set; }
        public Company Company { get; set; }
        public string Description { get; set; }
    }
}
