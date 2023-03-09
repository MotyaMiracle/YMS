using Microsoft.AspNetCore.Mvc;
using Domain.Services.Drivers;
using Microsoft.AspNetCore.Authorization;

namespace Yard_Management_System.Controllers
{
    [Authorize]
    [Route("api/driver")]
    [ApiController]
    public class DriversController : ControllerBase
    {
        private readonly IDriverService _driverService;
        public DriversController(IDriverService driverService)
        {
            _driverService = driverService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> Get(CancellationToken token)
        {
            return Ok(await _driverService.GetAllAsync(token));
        }

        [HttpGet("getDriver")]
        public async Task<IActionResult> Get(Guid driverId, CancellationToken token)
        {
            return Ok(await _driverService.GetAsync(driverId, token));
        }

        [HttpPost("createDriver")]
        public async Task<IActionResult> Post(DriverDto driverDto, CancellationToken token)
        {
            if (driverDto == null)
                return BadRequest();

            await _driverService.CreateAndUpdateAsync(driverDto, token);

            return Ok();
        }

        [HttpDelete("deleteDriver")]
        public async Task<IActionResult> Delete(Guid driverId, CancellationToken token)
        {
            await _driverService.DeleteDriverAsync(driverId, token);

            return Ok();
        }
    }
}
