namespace Domain.Services.Reports
{
    public interface IReportService
    {
        Task<ResponseReportDto> GetAsync(RequestReportDto reportDto, CancellationToken token);
    }
}
