using AutoMapper;
using Database;
using Domain.Services.Trucks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Yard_Management_System.Controllers
{
    [Authorize]
    [Route("api/truck")]
    [ApiController]
    public class TrucksController : Controller
    {
        private readonly ApplicationContext _db;
        private readonly IMapper _mapper;
        private readonly ITruckService _truckService;

        public TrucksController(ApplicationContext db, IMapper mapper, ITruckService truckService)
        {
            _db = db;
            _mapper = mapper;
            _truckService = truckService;
        }
        [HttpGet]
        public async Task<IActionResult> Get(Guid truckId, CancellationToken token)
        {
            return Ok (await _truckService.GetAsync(truckId, token));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAndUpdate(TruckDto truckDto, CancellationToken token)
        {
            if (truckDto == null)
            {
                return BadRequest();
            } 

            await _truckService.CreateAndUpdateAsync(truckDto,token);
            return Ok();
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll(CancellationToken token)
        {
            return Ok(await _truckService.GetAllAsync(token));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid truckId, CancellationToken token)
        {
            await _truckService.DeleteAsync(truckId, token);
            return Ok();
        }
    }
}
