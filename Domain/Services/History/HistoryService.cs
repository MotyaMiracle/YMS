namespace Domain.Services.History
{
    public class HistoryService : IHistoryService
    {
        public void Get(Guid routeId)
        {
            throw new NotImplementedException();
        }

        public Task GetAsync(Guid routeId, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public void Save(Guid routeId, string message)
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync(Guid routeId, CancellationToken token, string message)
        {
            throw new NotImplementedException();
        }
    }
}
