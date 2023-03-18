using AutoMapper;
using Database;
using Domain.Services.History;
using Domain.Services.Trips;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Yard_Management_System.Controllers
{
    [Authorize]
    [Route("api/trips")]
    [ApiController]
    public class TripsController : Controller
    {
        private readonly ApplicationContext _db;
        private readonly ITripService _tripService;
        private readonly IMapper _mapper;
        

        public TripsController(ApplicationContext db, IHistoryService historyService, IMapper mapper, ITripService tripService)
        {
            _db = db;
            _mapper = mapper;
            _tripService = tripService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(TripDto trip, CancellationToken token)
        {
            await _tripService.CreateAsync(trip, token);
            return Ok(trip);    
        }

        [HttpGet("startOperation/{tripId}")]
        public async Task<IActionResult> Operation(Guid tripId, CancellationToken token)
        {
            await _tripService.OperationAsync(tripId, token);
            return Ok();
        }
    }
}
