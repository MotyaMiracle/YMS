namespace Database.Entity
{
    public class Company
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid TruckId { get; set; }
        public Guid TrailerId { get; set; }
    }
}
