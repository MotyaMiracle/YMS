namespace Domain.Services.History
{
    public interface IHistoryService
    {
        Task<HistoryDto> GetAsync(Guid entityId, CancellationToken token);
        void Get(Guid entityId);
        Task SaveAsync (Guid entityId, string message, Guid userId, CancellationToken token);
        void Save (Guid entityId, string message);
    }
}
