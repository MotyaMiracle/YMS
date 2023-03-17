namespace Domain.Services.Timeslots
{
    public interface ITimeslotService
    {
        List<string> GetNotEmployedTimeslots();
        Task<TimeslotDto> CreateAsync(TimeslotDto timeslotDto, CancellationToken token);
    }
}
