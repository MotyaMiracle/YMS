namespace Yard_Management_System.Entity
{
    public class Driver
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        public string Patronymic { get; set; }
        public string Passport { get; set; }

        /// <summary>
        /// Дата выдачи паспорта
        /// </summary>
        public DateOnly DateOfIssuePassport { get; set; }

        /// <summary>
        /// Дата истечения пасспорта
        /// </summary>
        public DateOnly ExpirationDatePassport { get; set; }

        /// <summary>
        /// Водительское удостоверение
        /// </summary>
        public string DriveLicense { get; set; }
        public DateOnly DateOfIssueDriveLicense { get; set; }
        public DateOnly ExpirationDriveLicense { get; set; }
        public string PhoneNumber { get; set; }
    }
}
