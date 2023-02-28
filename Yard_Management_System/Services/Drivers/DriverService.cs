using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Yard_Management_System.Entity;

namespace Yard_Management_System.Services.Drivers
{
    public class DriverService : IDriverService
    {
        private readonly ApplicationContext _database;
        private readonly IMapper _mapper;

        public DriverService(ApplicationContext database, IMapper mapper)
        {
            _database = database;
            _mapper = mapper;
        }

        public async Task CreateAndUpdateAsync(DriverDto driverDto, CancellationToken token)
        {
            //Create
            if (string.IsNullOrWhiteSpace(driverDto.Id))
            {
                Driver driver = _mapper.Map<Driver>(driverDto);
                await _database.Drivers.AddAsync(driver, token);
            }
            //Update
            else
            {
                Driver updateDriver = _mapper.Map<Driver>(driverDto);
                _database.Drivers.Update(updateDriver);
            }
            await _database.SaveChangesAsync(token);
        }

        public async Task DeleteDriverAsync(Guid driverId, CancellationToken token)
        {
            Driver driver = await _database.Drivers
                .FirstOrDefaultAsync(d => d.Id == driverId, token);

            _database.Drivers.Remove(driver);
            await _database.SaveChangesAsync(token);
        }

        public async Task<DriverEntriesDto> GetAllAsync(CancellationToken token)
        {
            var drivers = await _database.Drivers.ToListAsync(token);

            return new DriverEntriesDto
            {
                Entries = _mapper.Map<IEnumerable<DriverDto>>(drivers).ToList()
                //Entries = drivers.Select(d => new DriverDto
                //{
                //    Id = d.Id.ToString(),
                //    Name = d.Name,
                //    Surname = d.Surname,
                //    Patronymic = d.Patronymic,
                //    Passport = d.Passport,
                //    DateOfIssuePassport = d.DateOfIssuePassport,
                //    ExpirationDatePassport = d.ExpirationDatePassport,
                //    DriveLicense = d.DriveLicense,
                //    DateOfIssueDriveLicense = d.DateOfIssueDriveLicense,
                //    ExpirationDriveLicense = d.ExpirationDriveLicense,
                //    PhoneNumber = d.PhoneNumber,
                //    AttachmentFilesId = d.AttachmentFilesId
                //}).ToList()
            };
        }

        public async Task<DriverDto> GetAsync(Guid driverId, CancellationToken token)
        {
            var driver = await _database.Drivers
                .FirstOrDefaultAsync(s => s.Id == driverId, token);

            var response = _mapper.Map<DriverDto>(driver);

            return response;
        }
    }
}
