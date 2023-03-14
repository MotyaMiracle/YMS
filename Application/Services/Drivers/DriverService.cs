using AutoMapper;
using Domain.Entity;
using Database;
using Domain.Services.Drivers;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Drivers
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

            if (driver is null)
                return;

            _database.Drivers.Remove(driver);
            await _database.SaveChangesAsync(token);
        }

        public async Task<IEnumerable<DriverDto>> GetAllAsync(CancellationToken token)
        {
            var drivers = await _database.Drivers.ToListAsync(token);

            return _mapper.Map<IEnumerable<DriverDto>>(drivers).ToList();
        }

        public async Task<DriverDto> GetAsync(Guid driverId, CancellationToken token)
        {
            var driver = await _database.Drivers
                .FirstOrDefaultAsync(s => s.Id == driverId, token);

            if (driver is null)
                return null;

            var response = _mapper.Map<DriverDto>(driver);

            return response;
        }
    }
}
