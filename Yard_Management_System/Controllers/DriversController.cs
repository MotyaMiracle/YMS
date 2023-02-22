using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Yard_Management_System.CRUDs.Driver;
using Yard_Management_System.Entity;

namespace Yard_Management_System.Controllers
{
    [Route("api/driver")]
    [ApiController]
    public class DriversController : ControllerBase
    {
        private readonly ApplicationContext _db;
        private readonly IMapper _mapper;

        public DriversController(ApplicationContext context, IMapper mapper)
        {
            _db = context;
            _mapper = mapper;
        }

        [HttpGet("getDrivers")]
        public async Task<IActionResult> Get(CancellationToken token)
        {
            var drivers = await _db.Drivers.ToListAsync(token);

            return Ok(drivers);
        }

        [HttpPost("createDriver")]
        public async Task<IActionResult> Post(CreateDriver newDriver, CancellationToken token)
        {
            if (newDriver == null)
                return BadRequest();
            //Create
            if (newDriver.Id == new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"))
            {
                Driver driver = _mapper.Map<Driver>(newDriver);
                driver.Id = Guid.NewGuid();
                await _db.Drivers.AddAsync(driver, token);
            }
            //Update
            else
            {
                Driver updateDriver = _mapper.Map<Driver>(newDriver);
                _db.Drivers.Update(updateDriver);
            }
            await _db.SaveChangesAsync(token);
            return Ok();
        }

        [HttpDelete("deleteDriver")]
        public async Task<IActionResult> Delete(Guid driveId, CancellationToken token)
        {
            if (driveId == Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"))
                return BadRequest();

            Driver driver = await _db.Drivers
                .FirstOrDefaultAsync(d => d.Id == driveId, token);

            if (driver == null)
                return NotFound();

            _db.Drivers.Remove(driver);
            await _db.SaveChangesAsync(token);
            return Ok();
        }
    }
}
