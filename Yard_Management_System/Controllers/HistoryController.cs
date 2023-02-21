using Domain.Services.History;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Yard_Management_System.Controllers
{
    [Route("api/history")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        IHistoryService _historyService;
        public HistoryController(IHistoryService historyService)
        {
            _historyService = historyService;
        }

        [HttpPost]
        public async Task Post(Guid entityId, string text, CancellationToken token)
        {
            var userId = Guid.Parse("5dd30af3-7fd4-4793-b777-2f48205138d0");
            await _historyService.SaveAsync(entityId, text, userId,token);
        }
    }
}
