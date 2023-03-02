using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Yard_Management_System.Entity;

namespace Yard_Management_System.Controllers
{
    [Authorize(Policy = "OnlyForAdmin")]
    [Route("api/roles")]
    [ApiController]
    public class RolesController : Controller
    {
        ApplicationContext db;

        public RolesController(ApplicationContext context)
        {
            db = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken token)
        {
            var roles = await db.Roles.ToListAsync(token);
            return Ok(roles);
        }
    }
}
