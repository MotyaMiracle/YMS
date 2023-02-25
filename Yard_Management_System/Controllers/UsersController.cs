using Domain.Services.Users;
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
        IUserService _userService;

        public UsersController(ApplicationContext context, IUserService userService)
        {
            db = context;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken token)
        {
            var users = await db.Users.Include(u => u.Role).ToListAsync(token);
            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> RegistrationAndUpdate(UserDto user, CancellationToken token)
        {
            await _userService.RegistrationAndUpdateAsync(user, token);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(Guid userId, CancellationToken token)
        {
            if (userId == new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"))
                return BadRequest();
            await _userService.DeleteUserAsync(userId, token);
            return Ok();
        }
    }
}
