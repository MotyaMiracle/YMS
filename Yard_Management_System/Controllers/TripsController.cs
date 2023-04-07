using AutoMapper;
using Database;
using Domain.Services.History;
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
        private readonly ApplicationContext _db;
        private readonly ITripService _tripService;
        private readonly IMapper _mapper;
        private readonly IStorageService _storageService;
        

        public TripsController(ApplicationContext db, IMapper mapper, ITripService tripService, IStorageService storageService)
        {
            _db = db;
            _mapper = mapper;
            _tripService = tripService;
            _storageService = storageService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(TripDto trip, CancellationToken token)
        {
            await _tripService.CreateAsync(trip, token);
            return Ok(trip);    
        }

        [HttpGet]
        public async Task<IActionResult> Operation(DateTime selectedDate, string storageName, CancellationToken token)
        {
               
            return Ok(await _storageService.GetExcpectedOccupancy(selectedDate, storageName, token));
        }
    }
}
