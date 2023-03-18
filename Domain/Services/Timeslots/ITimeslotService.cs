namespace Domain.Services.Timeslots
{
    public interface ITimeslotService
    {
        List<string> GetNotEmployedTimeslots(DateTime date, string gateName);
        Task<TimeslotDto> CreateAsync(TimeslotDto timeslotDto, string gateName, CancellationToken token);
    }
}
