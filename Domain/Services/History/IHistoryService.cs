namespace Domain.Services.History
{
    public interface IHistoryService
    {
        Task<HistoryDto> GetAsync(Guid tripId, CancellationToken token);
        void Get(Guid tripId);
        Task SaveAsync (Guid tripId, string message, Guid userId, CancellationToken token);
        void Save (Guid tripId, string message);

    }
}
