using Domain.Services.History;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Yard_Management_System.Controllers
{
    [Authorize]
    [Route("api/history")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        IHistoryService _historyService;
        public HistoryController(IHistoryService historyService)
        {
            _historyService = historyService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(Guid entityId, CancellationToken token)
        {
            var response = await _historyService.GetAsync(entityId, token);
            return Ok(response);
        }
    }
}
