using System.ComponentModel.DataAnnotations;

namespace Yard_Management_System.CRUDs.Driver
{
    public class CreateDriver
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        [Required]
        public string Passport { get; set; }
        [Required]
        public string DateOfIssuePassport { get; set; }
        [Required]
        public string ExpirationDatePassport { get; set; }
        [Required]
        public string DriveLicense { get; set; }
        [Required]
        public string DateOfIssueDriveLicense { get; set; }
        [Required]
        public string ExpirationDriveLicense { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public int AttachmentFilesId { get; set; }
    }
}
