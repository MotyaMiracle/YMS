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
    }
}
