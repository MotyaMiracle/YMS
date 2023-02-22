using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Yard_Management_System.CRUDs.Storage;
using Yard_Management_System.Entity;
using AutoMapper;

namespace Yard_Management_System.Controllers
{
    [Authorize]
    [Route("api/storage")]
    [ApiController]
    public class StorageController : ControllerBase
    {
        private readonly ApplicationContext _db;
        private readonly IMapper _mapper;

        public StorageController(ApplicationContext context, IMapper mapper) 
        {
            _db = context;
            _mapper = mapper;
        }

        [HttpGet("getStorage")]
        public async Task<IActionResult> Get(CancellationToken token) 
        {
            var storages = await _db.Storages.ToListAsync(token);

            return Ok(storages);
        }

        [HttpPost("createStorage")]
        public async Task<IActionResult> Post(CreateStorage newStorage, CancellationToken token)
        {
            if (newStorage == null)
                return BadRequest();

            //Create
            if (newStorage.Id == new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"))
            {
                Storage storage = _mapper.Map<Storage>(newStorage);
                storage.Id = Guid.NewGuid();
                await _db.Storages.AddAsync(storage, token);
            }
            //Update
            else
            {
                Storage updateStorage = _mapper.Map<Storage>(newStorage);
                _db.Storages.Update(updateStorage);
            }
            await _db.SaveChangesAsync(token);
            return Ok();
        }

        [HttpDelete("deleteStorage")]
        public async Task<IActionResult> Delete(Guid StorageId, CancellationToken token)
        {
            if (StorageId == Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"))
                return BadRequest();

            Storage storage = await _db.Storages
                .FirstOrDefaultAsync(s => s.Id == StorageId, token);

            if (storage == null)
                return NotFound();

            _db.Storages.Remove(storage);
            await _db.SaveChangesAsync(token);
            return Ok();
        }
    }
}
