using System.ComponentModel.DataAnnotations;

namespace Yard_Management_System.CRUDs.Driver
{
    public class CreateDriver
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        [Required]
        public string Passport { get; set; }
        [Required]
        public DateOnly DateOfIssuePassport { get; set; }
        [Required]
        public DateOnly ExpirationDatePassport { get; set; }
        [Required]
        public string DriveLicense { get; set; }
        [Required]
        public DateOnly DateOfIssueDriveLicense { get; set; }
        [Required]
        public DateOnly ExpirationDriveLicense { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public Guid AttachmentFilesId { get; set; }
    }
}
