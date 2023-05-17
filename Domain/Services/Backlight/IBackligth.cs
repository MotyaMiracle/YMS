using Domain.Enums;

namespace Domain.Services.Color
{
    public interface IBackligth
    {
        BacklightType Type { get; set; }
        Task<bool> IsActive(string entityId, CancellationToken token);
    }
}
