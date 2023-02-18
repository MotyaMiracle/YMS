using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Yard_Management_System.CRUDs.Storage;
using Yard_Management_System.Entity;

namespace Yard_Management_System.Controllers
{
    [Authorize]
    [Route("api/storage")]
    [ApiController]
    public class StorageController : ControllerBase
    {
        ApplicationContext _db;
        
        public StorageController(ApplicationContext context) 
        {
            _db = context;
        }

        [HttpGet("getStorage")]
        public async Task<IActionResult> Get(CancellationToken token) 
        {
            var storages = await _db.Storages.ToListAsync(token);

            return Ok(storages);
        }

        [HttpPost("createStorage")]
        public async Task<IActionResult> Post(CreateStorage newStorage,CancellationToken token)
        {
            if (newStorage == null)
                return BadRequest();

            Storage storage = new Storage
            {
                Id = Guid.NewGuid(),
                Name = newStorage.Name,
                Address = newStorage.Address,
                Coordinates = newStorage.Coordinates,
                OpeningHours = newStorage.OpeningHours,
                DayOfWeeks = newStorage.DayOfWeeks,
            };

            await _db.Storages.AddAsync(storage, token);
            await _db.SaveChangesAsync(token);
            return Ok(storage);
        }

        [HttpDelete("deleteStorage")]
        public async Task<IActionResult> Delete(DeleteStorage deleteStorage, CancellationToken token)
        {
            if (deleteStorage == null)
                return BadRequest();

            Storage storage = await _db.Storages
                .FirstOrDefaultAsync(s => s.Name == deleteStorage.Name, token);

            if (storage == null)
                return NotFound();

            _db.Storages.Remove(storage);
            await _db.SaveChangesAsync(token);
            return Ok();
        }

        [HttpPut("updateStorage")]
        public async Task<IActionResult> Put(UpdateStorage updateStorage, CancellationToken token)
        {
            if (updateStorage == null)
                return BadRequest();

            Storage storage = await _db.Storages
                .FirstOrDefaultAsync(s => s.Name == updateStorage.OldName, token);

            if(storage == null)
                return NotFound();

            if(updateStorage.NewName != "string")
                storage.Name = updateStorage.NewName;

            if(updateStorage.NewAddress != "string")
                storage.Address = updateStorage.NewAddress;

            if(updateStorage.NewCoordinates.Any())
                storage.Coordinates = updateStorage.NewCoordinates;

            if(updateStorage.NewOpeningHours != "string")
                storage.OpeningHours = updateStorage.NewOpeningHours;

            if(updateStorage.NewDayOfWeeks.Any())
                storage.DayOfWeeks = updateStorage.NewDayOfWeeks;

            _db.Storages.Update(storage);
            await _db.SaveChangesAsync(token);
            return Ok();
        }
    }
}
