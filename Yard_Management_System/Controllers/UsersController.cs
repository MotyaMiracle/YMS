using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    }
}
