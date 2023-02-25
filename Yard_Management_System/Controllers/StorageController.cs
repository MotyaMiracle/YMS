using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Yard_Management_System.Services.Storages;
using Yard_Management_System.Entity;
using AutoMapper;

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
            if (storageId == Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"))
                return BadRequest();

            await _storageService.DeleteStorageAsync(storageId, token);

            return Ok();
        }
    }
}
