using Domain.Entity;

namespace Domain.Services.Drivers
{
    public interface IDriverService
    {
        Task<DriverDto> GetAsync(Guid driverId, CancellationToken token);

        Task CreateAndUpdateAsync(DriverDto driverDto, CancellationToken token);

        Task DeleteDriverAsync(Guid driverId, CancellationToken token);

        Task<IEnumerable<DriverDto>> GetAllAsync(CancellationToken token);
    }
}
