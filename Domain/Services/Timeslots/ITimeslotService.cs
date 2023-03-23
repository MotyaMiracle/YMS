using Domain.Services.Trips;

namespace Domain.Services.Timeslots
{
    public interface ITimeslotService
    {
        Task<EntryTimeslotView> GetTimeslotsAsync(Guid tripId, DateTime selectedDate, CancellationToken token);
        Task<TimeslotDto> CreateAsync(TimeslotDto timeslotDto, string gateName, CancellationToken token);
    }
}
