using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Yard_Management_System.Entity;

namespace Yard_Management_System.Controllers
{
    [Authorize(Policy = "OnlyForAdmin")]
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        ApplicationContext db;

        public UsersController(ApplicationContext context)
        {
            db = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken token)
        {
            var users = await db.Users.Include(u => u.Role).ToListAsync(token);
            return Ok(users);
        }

        [HttpPut("update")]
        public async Task<ActionResult<User>> Edit(UpdateUser updateUser)
        {
            if (updateUser != null)
            {
                User? user = await db.Users.FirstOrDefaultAsync(p => p.Login == updateUser.Login);
                if (user != null)
                {
                    if (updateUser.NewLogin != "string") 
                    {
                        user.Login = updateUser.NewLogin;
                    }
                    if (updateUser.NewPhoneNumber != "string")
                    {
                        user.PhoneNumber = updateUser.NewPhoneNumber;
                    }
                    if (updateUser.NewEmail != "string")
                    {
                        user.Email = updateUser.NewEmail;
                    }
                    if (updateUser.NewPassword != "string")
                    {
                        user.Password = updateUser.NewPassword;
                        user.PasswordHash = Authorization.GetHash(user.Password);
                    }
                    
                    db.Users.Update(user);
                    await db.SaveChangesAsync();
                    return Ok(user);
                }
                    
            }
            return NotFound();
        }
    }
}
