using System.ComponentModel.DataAnnotations;

<<<<<<<< HEAD:Domain/Services/Authorization/LoginDto.cs
namespace Domain.Services.Authorization
========
namespace Yard_Management_System.CRUDs.Authorization
>>>>>>>> b881240f371d2ede5e08f9de1b79730eb4d4396f:Yard_Management_System/CRUDs/Authorization/LoginDto.cs
{
    public class LoginDto
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
