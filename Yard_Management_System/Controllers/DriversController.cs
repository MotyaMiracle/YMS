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
        ApplicationContext _db;

        public DriversController(ApplicationContext context)
        {
            _db = context;
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

            Driver driver = new Driver
            {
                Id = Guid.NewGuid(),
                Name = newDriver.Name,
                Surname = newDriver.Surname,
                Patronymic = newDriver.Patronymic,
                Passport = newDriver.Passport,
                DateOfIssuePassport = DateOnly.Parse(newDriver.ExpirationDatePassport),
                ExpirationDatePassport = DateOnly.Parse(newDriver.ExpirationDatePassport),
                DriveLicense = newDriver.DriveLicense,
                DateOfIssueDriveLicense = DateOnly.Parse(newDriver.ExpirationDriveLicense),
                ExpirationDriveLicense = DateOnly.Parse(newDriver.ExpirationDriveLicense),
                PhoneNumber = newDriver.PhoneNumber,
                AttachmentFilesId = newDriver.AttachmentFilesId
            };

            await _db.Drivers.AddAsync(driver, token);
            await _db.SaveChangesAsync(token);
            return Ok();
        }

        [HttpDelete("deleteDriver")]
        public async Task<IActionResult> Delete(DeleteDriver deleteDriver, CancellationToken token)
        {
            if(deleteDriver == null) 
                return BadRequest();

            Driver driver = await _db.Drivers
                .FirstOrDefaultAsync(d => d.Passport == deleteDriver.Passport, token);

            if (driver == null)
                return NotFound();

            _db.Drivers.Remove(driver);
            await _db.SaveChangesAsync(token);
            return Ok();
        }

        [HttpPut("updateDriver")]
        public async Task<IActionResult> Put(UpdateDriver updateDriver, CancellationToken token)
        {
            if (updateDriver == null)
                return BadRequest();

            Driver driver = await _db.Drivers
                .FirstOrDefaultAsync(d => d.Passport == updateDriver.OldPassport, token);

            if (driver == null)
                return NotFound();

            if(driver.Name != "string")
                driver.Name = updateDriver.Name;

            if (driver.Surname != "string")
                driver.Surname = updateDriver.Surname;

            if (driver.Patronymic != "string")
                driver.Patronymic = updateDriver.Patronymic;

            if (driver.Passport != "string")
            {
                driver.Passport = updateDriver.Passport;
                driver.DateOfIssuePassport = DateOnly.Parse(updateDriver.DateOfIssuePassport);
                driver.ExpirationDatePassport = DateOnly.Parse(updateDriver.ExpirationDatePassport);
            }
                
            if (driver.DriveLicense != "string")
            {
                driver.DriveLicense = updateDriver.DriveLicense;
                driver.DateOfIssueDriveLicense = DateOnly.Parse(updateDriver.DateOfIssueDriveLicense);
                driver.DateOfIssueDriveLicense = DateOnly.Parse(updateDriver.DateOfIssueDriveLicense);
            }

            if (driver.PhoneNumber != "string")
                driver.PhoneNumber = updateDriver.PhoneNumber;

            if (driver.AttachmentFilesId != 0)
                driver.AttachmentFilesId = updateDriver.AttachmentFilesId;

            _db.Drivers.Update(driver);
            await _db.SaveChangesAsync(token);
            return Ok();

        }
    }
}
