using Domain.Services.Reports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Yard_Management_System.Controllers
{
    [Authorize]
    [Route("api/report")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService) 
        {
            _reportService= reportService;
        }

        [HttpPost]
        public async Task<IActionResult> Options(RequestReportDto reportDto, CancellationToken token)
        {
            return Ok( await _reportService.GetAsync(reportDto, token));
        }
    }
}
