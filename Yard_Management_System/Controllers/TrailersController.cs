using AutoMapper;
using Database.Entity;
using Domain.Services.Trailers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Yard_Management_System.Controllers
{
    [Authorize]
    [Route("api/trailers")]
    [ApiController]
    public class TrailersController : Controller
    {
        private readonly ApplicationContext _db;
        private readonly IMapper _mapper;
        private readonly ITrailerService _trailerService;

        public TrailersController(ApplicationContext db, IMapper mapper, ITrailerService trailerService)
        {
            _db = db;
            _mapper = mapper;
            _trailerService = trailerService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(Guid trailerId, CancellationToken token)
        {
            return Ok(await _trailerService.GetAsync(trailerId, token));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAndUpdate(TrailerDto trailerDto, CancellationToken token)
        {
            if (trailerDto == null)
            {
                return BadRequest();
            }

            await _trailerService.CreateAndUpdateAsync(trailerDto, token);
            return Ok();
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll(CancellationToken token)
        {
            return Ok(await _trailerService.GetAllAsync(token));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid trailerId, CancellationToken token)
        {
            await _trailerService.DeleteAsync(trailerId, token);
            return Ok();
        }
    }
}
