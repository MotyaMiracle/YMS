using Domain.Services.History;
using Microsoft.AspNetCore.Mvc;
using Yard_Management_System.Entity;


namespace Yard_Management_System.Controllers
{
    [Route("api/routes")]
    [ApiController]
    public class RoutesController : Controller
    {
        ApplicationContext db;
        IHistoryService historyService;

        public RoutesController(ApplicationContext context)
        {
            db = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CancellationToken token, Trip route)
        {
            if (route == null)
                return BadRequest();
            route.Id = Guid.NewGuid();
            await db.Trips.AddAsync(route, token);
            await db.SaveChangesAsync(token);
            return Ok(route);
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken token)
        {
            return Ok(token);
        }
    }
}
