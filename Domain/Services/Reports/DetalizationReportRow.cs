namespace Domain.Services.Reports
{
    public class DetalizationReportRow
    {
        public string DetailType { get; set; }
        public int TripsCount { get; set; 
        public List<DetalizationReportRow> SubRows { get; set; }
    }
}
