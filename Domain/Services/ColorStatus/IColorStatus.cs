namespace Domain.Services.Color
{
    public interface IColorStatus
    {
        Task<ColorStatusDto> TruckStatusAsync(string carNumber, CancellationToken token);
    }
}
