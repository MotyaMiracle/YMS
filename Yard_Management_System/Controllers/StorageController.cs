using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Domain.Services.Storages;

namespace Yard_Management_System.Controllers
{
    [Authorize]
    [Route("api/storage")]
    [ApiController]
    public class StorageController : ControllerBase
    {
        private readonly IStorageService _storageService;

        public StorageController(IStorageService storageService)
        {
            _storageService = storageService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> Get(CancellationToken token)
        {
            return Ok(await _storageService.GetAllAsync(token));
        }

        [HttpGet("getStorage")]
        public async Task<IActionResult> Get(Guid storageId, CancellationToken token)
        {
            return Ok(await _storageService.GetAsync(storageId, token));
        }

        [HttpPost("createStorage")]
        public async Task<IActionResult> Post(StorageDto newStorage, CancellationToken token)
        {
            if (newStorage == null)
                return BadRequest();

            await _storageService.CreateAndUpdateAsync(newStorage, token);

            return Ok();
        }

        [HttpDelete("deleteStorage")]
        public async Task<IActionResult> Delete(Guid storageId, CancellationToken token)
        {

            await _storageService.DeleteStorageAsync(storageId, token);

            return Ok();
        }

        [HttpGet("{carNumber}")]
        public async Task<IActionResult> CanDrive(string carNumber, CancellationToken token)
        {
            return Ok(await _storageService.CanDriveToStorageAsync(carNumber, token));
        }

        [HttpPost("QRCode")]
        public async Task<IActionResult> CanDriveQrCode(IFormFile formFile, CancellationToken token)
        {
            return Ok(await _storageService.CanDriveToStorageQRCodeAsync(formFile, token));
        }
    }
}
