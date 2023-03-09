namespace Domain.Services.Gates
{
    public interface IGatesService
    {
        Task<GateDto> GetAsync(Guid gateId, CancellationToken token);

        Task CreateAndUpdateAsync(GateDto gateDto, CancellationToken token);

        Task DeleteGateAsync(Guid gateId, CancellationToken token);

        Task<IEnumerable<GateDto>> GetAllAsync(CancellationToken token);

        public Task<bool> CanDriveToGateAsync(string carNumber, CancellationToken token);
    }
}
