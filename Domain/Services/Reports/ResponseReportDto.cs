using Domain.Services.History;
using Domain.Shared;

namespace Domain.Services.Reports
{
    public class ResponseReportDto
    {
        public List<DetalizationReportRow> Entries { get; set; }
        
    }
}
