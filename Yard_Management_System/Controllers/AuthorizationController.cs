using Domain.Services.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Yard_Management_System.Entity;

namespace Yard_Management_System.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        ApplicationContext _db;

        public AuthorizationController(ApplicationContext db)
        {
            _db = db;
        }

        [HttpPost]
        public async Task<ActionResult<User>> Post(LoginDto dto, CancellationToken token)
        {
            User user = await _db.Users.Include(u => u.Role)
                                       .FirstOrDefaultAsync(p => p.Login == dto.Login && p.PasswordHash == Authorization.GetHash(dto.Password), token);
            
            if (user is null)
                return Unauthorized();

            var identity = Authorization.GetIdentity(dto, user) ;
            var claims = identity.Claims.ToList();
            var encodedJwt = Authorization.GenerateJwtToken(claims, TimeSpan.FromDays(7));
            var claimsPrincipal = new ClaimsPrincipal(identity);

            var response = new
            {
                userRole = user.Role?.Name,
                Token = encodedJwt
            };

            return Ok(response);
        }
    }
}
