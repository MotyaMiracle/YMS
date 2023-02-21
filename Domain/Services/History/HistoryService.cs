using Database.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Yard_Management_System;

namespace Domain.Services.History
{
    public class HistoryService : IHistoryService
    {
        ApplicationContext _db;

        public HistoryService(ApplicationContext db)
        {
            _db = db;
        }

        public void Get(Guid tripId)
        {
            throw new NotImplementedException();
        }

        public async Task<HistoryDto> GetAsync(Guid entityId, CancellationToken token)
        {
            var historyEntries = await _db.HistoryEntries
               .Include(x => x.User)
               .Where(x => x.EntityId == entityId)
               .ToListAsync(token);

            return new HistoryDto
            {
                Entries = historyEntries.Select(x => new HistoryEntryDto
                {
                    Date = x.Date,
                    Text = x.Text,
                    UserName = x.User.Login,
                }).ToList()
            };
        }

        public void Save(Guid tripId, string message)
        {
            throw new NotImplementedException();
        }

        public async Task SaveAsync(Guid entityId, string message, Guid userId, CancellationToken token)
        {
            var entry = new HistoryEntry
            {
                Date = DateTime.UtcNow,
                Text = message,
                UserId = userId,
                EntityId = entityId
            };

            await _db.HistoryEntries.AddAsync(entry, token);
            await _db.SaveChangesAsync(token);
        }
    }
}
