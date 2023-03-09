namespace Database.Entity
{
    public class Trailer
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
        public string Description { get; set; }
        public Guid CompanyId { get; set; }
        public Company Company { get; set; }

        /// <summary>
        /// Грузоподьемность
        /// </summary>
        public string CargoCapacity { get; set; }
    }
}
