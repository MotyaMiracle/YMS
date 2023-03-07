namespace Database.Entity
{
    public class Trailer
    {
        public Guid Id { get; set; }
        public string TrailerNumber { get; set; }
        public string Description { get; set; }
        public Guid CompanyId { get; set; }

        /// <summary>
        /// Грузоподьемность
        /// </summary>
        public string CargoCapacity { get; set; }
    }
}
