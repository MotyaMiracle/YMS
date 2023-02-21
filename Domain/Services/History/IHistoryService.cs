using Database.Entity;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.History
{
    public interface IHistoryService
    {
        Task<HistoryDto> GetAsync(Guid tripId, CancellationToken token);
        void Get(Guid tripId);
        Task SaveAsync (Guid tripId, string message, Guid userId, CancellationToken token);
        void Save (Guid tripId, string message);

    }
}
