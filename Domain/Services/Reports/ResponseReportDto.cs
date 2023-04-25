using Domain.Services.History;
using Domain.Shared;

namespace Domain.Services.Reports
{
    public class ResponseReportDto
    {
        public List<ReportEntryDto> Entries { get; set; }
        public int TripsCount { get; set; }
    }
}
