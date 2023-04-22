using Domain.Enums;

namespace Domain.Services.Trucks
{
    public interface ITruckService
    {
        Task CreateAndUpdateAsync(TruckDto truckDto, CancellationToken token);
        Task<TruckDto> GetAsync(Guid truckId, CancellationToken token);
        Task<IEnumerable<TruckDto>> GetAllAsync(CancellationToken token);
        Task DeleteAsync(Guid truckId, CancellationToken token);
        Task<ColorStatus?> TruckStatusAsync(string carNumber, CancellationToken token);
    }
}
