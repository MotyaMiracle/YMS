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

        [HttpGet("list")]
        public async Task<IActionResult> Get(CancellationToken token)
        {
            var users = await db.Users.Include(u => u.Role).ToListAsync(token);
            return Ok(users);
        }

        [HttpPost("registration")]
        public async Task<IActionResult> Put(RegisterNewUser newUser, CancellationToken token)
        {
            if (newUser == null)
                return BadRequest();

            User user = new User { 
                Id = Guid.NewGuid(), 
                Login = newUser.Login,
                Email = newUser.Email,
                Password = newUser.Password,
                PasswordHash = Authorization.GetHash(newUser.Password),
                IsActive = false,
                PhoneNumber = newUser.Phone,
                RoleId = GenerationRoleId.ReceptionistId
            };

            await db.Users.AddAsync(user, token);
            await db.SaveChangesAsync(token);
            return Ok();
        }
        [HttpDelete("remove")]
        public async Task<IActionResult> Delete(DeleteUser deleteUser, CancellationToken token)
        {
            if(deleteUser == null)
                return BadRequest();

            User user = await db.Users
                .FirstOrDefaultAsync(u => u.Login == deleteUser.Login, token);

            if(user == null)
                return NotFound();

            db.Users.Remove(user);
            await db.SaveChangesAsync(token);
            return Ok();
        }
    }
}
