namespace Domain.Services.Trucks
{
    public interface ITruckService
    {
        Task CreateAndUpdateAsync(TruckDto truckDto, CancellationToken token);
        Task<TruckDto> GetAsync(Guid truckId, CancellationToken token);
        Task<TruckEntriesDto> GetAllAsync(CancellationToken token);
        Task DeleteAsync(Guid truckId, CancellationToken token);
    }
}
