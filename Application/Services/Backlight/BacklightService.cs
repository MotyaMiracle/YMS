using Database;
using Domain.Entity;
using Domain.Enums;
using Domain.Services.Color;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Status
{
    public class BacklightService : IBackligth
    {
        private readonly ApplicationContext _db;

        public BacklightService(ApplicationContext db)
        {
            _db = db;
        }

        public BacklightType Type { get; set ; }

        public async Task<bool> IsActive(string entityId, CancellationToken token)
        {
            DateTime arrivalTime = DateTime.UtcNow;

            bool changes = false;

            Trip trip = await _db.Trips
                .Include(t => t.Truck)
                .Include(t => t.Timeslot)
                .FirstOrDefaultAsync(t => t.Id == Guid.Parse(entityId));

            if (trip == null)
                return false;

            if (trip.ArrivalTime < arrivalTime)
            {
                Type = BacklightType.BeLate;
                changes = true;
            }

            if (DateTime.Parse(trip.Timeslot.To) <= arrivalTime + new TimeSpan(3, 5, 0) &&
                trip.Timeslot.Date.Day == arrivalTime.Day)
            {
                Type = BacklightType.NeedToSpeedUp;
                changes = true;
            }

            trip.Backlights = Type.ToString();

            _db.SaveChangesAsync(token);

            return changes;
        }
    }
}
