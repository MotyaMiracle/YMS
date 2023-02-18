using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Yard_Management_System.Entity;

namespace Yard_Management_System.Controllers
{
    [Route("api/routes")]
    [ApiController]
    public class RoutesController : Controller
    {
        ApplicationContext db;
        public RoutesController(ApplicationContext context)
        {
            db = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CancellationToken token, Road route)
        {
            if (route == null)
                return BadRequest();
            route.Id = Guid.NewGuid();
            await db.Routes.AddAsync(route, token);
            await db.SaveChangesAsync(token);
            return Ok(route);
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken token)
        {
            var routes = await db.Routes.ToListAsync(token);
            return Ok(routes);
        }
    }
}
