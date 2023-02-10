using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Yard_Management_System.Models;

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
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            var users = await db.Users.Include(u => u.Role).ToListAsync();
            string usersToString = "";
            foreach (var u in users)
            {
                usersToString = $"{usersToString}\nLogin: {u.Login}, Email: {u.Email}, Role: {u.Role?.Name}, HashPassword: {u.PasswordHash}";
            }
            return new ObjectResult(usersToString);

        }
    }
}
