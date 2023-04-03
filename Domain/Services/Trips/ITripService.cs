namespace Domain.Services.Trips
{
    public interface ITripService
    {
        Task CreateAsync (TripDto trip, CancellationToken token);
        Task OccupancyAsync(Guid tripId,CancellationToken token);
        void Create(Guid entityId);
    }
}
