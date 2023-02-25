using AutoMapper;
using Domain.Services.History;
using Domain.Services.Trips;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Yard_Management_System.Entity;


namespace Yard_Management_System.Controllers
{
    [Route("api/trips")]
    [ApiController]
    public class TripsController : Controller
    {
        private readonly ApplicationContext _db;
        private readonly IHistoryService _historyService;
        private readonly ITripService _tripService;
        private readonly IMapper _mapper;
        

        public TripsController(ApplicationContext db, IHistoryService historyService, IMapper mapper, ITripService tripService)
        {
            _db = db;
            _historyService = historyService;
            _mapper = mapper;
            _tripService = tripService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(TripDto trip, CancellationToken token)
        {
            await _tripService.CreateAsync(trip, token);
            return Ok(trip);    
        }

        [HttpPost("toHistory")]
        public async Task Post(Guid entityId, string text, CancellationToken token)
        {
            var userId = Guid.Parse("5dd30af3-7fd4-4793-b777-2f48205138d0");
            await _historyService.SaveAsync(entityId, text, userId, token);
        }
    }
}
