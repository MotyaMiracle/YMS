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
        /// ИНН
        /// </summary>
        public string Inn { get; set; }
    }
}
