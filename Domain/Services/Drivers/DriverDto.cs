using System.ComponentModel.DataAnnotations;

namespace Domain.Services.Drivers
{
    public class DriverDto
    {
        public string Id { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        [Required]
        public string Surname { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        public string Patronymic { get; set; }

        /// <summary>
        /// Пасспорт
        /// </summary>
        [Required]
        public string Passport { get; set; }

        /// <summary>
        /// Дата выдачи паспорта
        /// </summary>
        [Required]
        public DateOnly DateOfIssuePassport { get; set; }

        /// <summary>
        /// Дата истечения пасспорта
        /// </summary>
        [Required]
        public DateOnly ExpirationDatePassport { get; set; }

        /// <summary>
        /// Водительское удостоверение
        /// </summary>
        [Required]
        public string DriveLicense { get; set; }

        /// <summary>
        /// Дата выдачи водительского удостоверения
        /// </summary>
        [Required]
        public DateOnly DateOfIssueDriveLicense { get; set; }

        /// <summary>
        /// Дата истечения водительского удостоверения
        /// </summary>
        [Required]
        public DateOnly ExpirationDriveLicense { get; set; }

        /// <summary>
        /// Номер телефона
        /// </summary>
        [Required]
        public string PhoneNumber { get; set; }
    }
}
