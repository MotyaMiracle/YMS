namespace Domain.Services.Trips
{
    public interface ITripService
    {
        Task CreateAsync (TripDto trip, CancellationToken token);
        Task OperationAsync(Guid tripId, CancellationToken token);
        void Create(Guid entityId);
    }
}
