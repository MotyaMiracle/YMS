using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Claims;
using Yard_Management_System.Models;

namespace Yard_Management_System.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        ApplicationContext db;
        public AuthorizationController(ApplicationContext db)
        {
            this.db = db;
        }
        [HttpPost]
        public async Task<ActionResult<User>> Post()
        {
            var form = Request.Form;

            if (!form.ContainsKey("login") || !form.ContainsKey("password"))
                return BadRequest("Неверный логин или пароль");

            string login = form["login"];
            string password = form["password"];

            User user = await db.Users.Include(u => u.Role).FirstOrDefaultAsync(p => p.Login == login && p.Password == password);
            if (user is null) return Unauthorized();
            // Костыль чтобы задать хешированный пароль, а то при HasData не получалось
            if (user.PasswordHash == null)
            {
                user.PasswordHash = Authorization.GetHash(user.Password);
                await db.SaveChangesAsync();
            }
            var identity = Authorization.GetIdentity(user);
            var claims = identity.Claims.ToList();
            var encodedJwt = Authorization.GenerateJwtToken(claims, TimeSpan.FromDays(7));
            var claimsPrincipal = new ClaimsPrincipal(identity);
            var response = new
            {
                userRole = user.Role?.Name,
                Token = encodedJwt
            };
            return new ObjectResult(response);
        }
    }
}
