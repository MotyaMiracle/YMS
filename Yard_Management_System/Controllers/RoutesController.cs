using Domain.Services.History;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Yard_Management_System.Entity;


namespace Yard_Management_System.Controllers
{
    [Route("api/routes")]
    [ApiController]
    public class RoutesController : Controller
    {
        ApplicationContext _db;
        private readonly IHistoryService _historyService;
        

        public RoutesController(ApplicationContext db, IHistoryService historyService)
        {
            _db = db;
            _historyService = historyService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CancellationToken token)
        {
            Trip trip = new Trip
            {
                Id = Guid.NewGuid(),
                StorageId = Guid.NewGuid(),
                DriverId = Guid.NewGuid(),
                ArrivalTime = DateTime.UtcNow,
                NowStatus = Trip.Status.Create
            };
            
            await _db.Trips.AddAsync(trip, token);
            await _db.SaveChangesAsync(token);
            return Ok(trip);
        }

        [HttpGet]
        public async Task<IActionResult> Get(Guid entityId,CancellationToken token)
        {
            var response = await _historyService.GetAsync(entityId, token);
            return Ok(response);
        }
    }
}
