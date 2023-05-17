using Domain.Services.Gates;
using Domain.Services.Storages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Yard_Management_System.Controllers
{
    [Route("api/gates")]
    [ApiController]
    public class GatesController : ControllerBase
    {
        private readonly IGatesService _gatesService;

        public GatesController(IGatesService gatesService)
        {
            _gatesService = gatesService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> Get(CancellationToken token)
        {
            return Ok(await _gatesService.GetAllAsync(token));
        }

        [HttpGet("getGate")]
        public async Task<IActionResult> Get(Guid gateId, CancellationToken token)
        {
            return Ok(await _gatesService.GetAsync(gateId, token));
        }

        [HttpPost("createGate")]
        public async Task<IActionResult> Post(GateDto newGate, CancellationToken token)
        {
            if (newGate == null)
                return BadRequest();

            await _gatesService.CreateAndUpdateAsync(newGate, token);

            return Ok();
        }

        [HttpDelete("deleteGate")]
        public async Task<IActionResult> Delete(Guid gateId, CancellationToken token)
        {
            await _gatesService.DeleteGateAsync(gateId, token);

            return Ok();
        }

        [HttpGet("{carNumber}")]
        public async Task<IActionResult> CanDrive(string carNumber, CancellationToken token)
        {
            return Ok(await _gatesService.CanDriveToGateAsync(carNumber, token));
        }

        [HttpPost("QRCode")]
        public async Task<IActionResult> CanDriveQrCode(IFormFile formFile, CancellationToken token)
        {
            return Ok(await _gatesService.CanDriveToGateQRCodeAsync(formFile, token));
        }
    }
}
