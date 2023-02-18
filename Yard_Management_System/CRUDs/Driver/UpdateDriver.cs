using System.ComponentModel.DataAnnotations;

namespace Yard_Management_System.CRUDs.Driver
{
    public class UpdateDriver
    {
        [Required]
        public string OldPassport { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Passport { get; set; }
        public string DateOfIssuePassport { get; set; }
        public string ExpirationDatePassport { get; set; }
        public string DriveLicense { get; set; }
        public string DateOfIssueDriveLicense { get; set; }
        public string ExpirationDriveLicense { get; set; }
        public string PhoneNumber { get; set; }
        public int AttachmentFilesId { get; set; }
    }
}
