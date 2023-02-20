using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.History
{
    internal interface IHistoryService
    {
        Task GetAsync(Guid routeId, CancellationToken token);
        void Get(Guid routeId);
        Task SaveAsync (Guid routeId, CancellationToken token, string message);
        void Save (Guid routeId, string message);

    }
}
