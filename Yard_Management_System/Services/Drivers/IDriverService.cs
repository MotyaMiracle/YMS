using Yard_Management_System.Entity;

namespace Yard_Management_System.Services.Drivers
{
    public interface IDriverService
    {
        Task<DriverDto> GetAsync(Guid driverId, CancellationToken token);

        Task CreateAndUpdateAsync(DriverDto driverDto, CancellationToken token);

        Task DeleteDriverAsync(Guid driverId, CancellationToken token);

        Task<DriverEntriesDto> GetAllAsync(CancellationToken token);
    }
}
