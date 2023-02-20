using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yard_Management_System;
using Yard_Management_System.Entity;

namespace Domain.Services.History
{
    internal class HistoryService : IHistoryService
    {
        private readonly ApplicationContext _database;

        public HistoryService(ApplicationContext database) 
        { 
            _database = database;
        }
        public async Task GetAsync(Guid routeId, CancellationToken token)
        {
            Road route 
            
        }

        public async Task SaveAsync()
    }
}
