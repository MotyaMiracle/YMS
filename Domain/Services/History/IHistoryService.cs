using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.History
{
    public interface IHistoryService
    {
        Task GetAsync(Guid tripId, CancellationToken token);
        void Get(Guid tripId);
        Task SaveAsync (Guid tripId, CancellationToken token, string message);
        void Save (Guid tripId, string message);

    }
}
