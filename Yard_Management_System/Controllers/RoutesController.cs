using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken token)
        {
            var routes = await db.Routes.ToListAsync(token);
            return Ok(routes);
        }
    }
}
