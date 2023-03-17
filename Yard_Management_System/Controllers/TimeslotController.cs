using Domain.Services.Timeslots;
using Microsoft.AspNetCore.Mvc;

namespace Yard_Management_System.Controllers
{
    [Route("api/timeslot")]
    [ApiController]
    public class TimeslotController : Controller
    {
        private readonly ITimeslotService _timeslotService;

        public TimeslotController(ITimeslotService timeslotService)
        {
            _timeslotService = timeslotService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {    
            return Ok(_timeslotService.GetNotEmployedTimeslots());
        }

        [HttpPost]
        public async Task<IActionResult> Create(TimeslotDto timeslotDto, CancellationToken token)
        {
            
            return Ok(await _timeslotService.CreateAsync(timeslotDto, token));
        }
    }
}
