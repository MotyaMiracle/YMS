using AutoMapper;
using Database;
using Domain.Services.Storages;
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
        private readonly ITripService _tripService;
        private readonly IStorageService _storageService;
        

        public TripsController(ITripService tripService, IStorageService storageService)
        {
            _tripService = tripService;
            _storageService = storageService;
        }

        [HttpPost("createTrip")]
        public async Task<IActionResult> Create(TripDto trip, CancellationToken token)
        {
            await _tripService.CreateAsync(trip, token);
            return Ok(trip);    
        }

        [HttpGet("getTrip")]
        public async Task<IActionResult> Get(Guid tripId, CancellationToken token)
        {
            return Ok(await _tripService.GetAsync(tripId, token));
        }

        [HttpGet("expectedOccupancy")]
        public async Task<IActionResult> Operation(DateTime selectedDate, string? storageName, CancellationToken token)
        {
               
            return Ok(await _storageService.GetExcpectedOccupancy(selectedDate, storageName, token));
        }

        [HttpGet("backlight")]
        public async Task<IActionResult> Backlight(string entityId, CancellationToken token)
        {
            return Ok(await _tripService.BackligthAsync(entityId, token));
        }
    }
}
