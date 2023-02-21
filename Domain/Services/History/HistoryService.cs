using Yard_Management_System;
using Yard_Management_System.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Domain.Services.History
{
    public class HistoryService : IHistoryService
    {
        ApplicationContext _db;

        public void Get(Guid tripId)
        {
            Trip trip = _db.Trips.FirstOrDefault(t => t.Id == tripId);
            return;
        }

        public async Task<HistoryDto> GetAsync(Guid tripId, CancellationToken token)
        {
            var trip = await _db.Trips.FirstOrDefault(t => t.Id == tripId);
            
            return new HistoryDto
            {
                Entries = trip
            };
        }

        public void Save(Guid tripId, string message)
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync(Guid tripId, CancellationToken token, string message)
        {
            throw new NotImplementedException();
        }
    }
}
