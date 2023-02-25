using Yard_Management_System.Entity;
namespace Domain.Services.Trips
{
    public interface ITripService
    {
        Task CreateAsync (TripDto trip, CancellationToken token);
        void Create(Guid entityId);
    }
}
