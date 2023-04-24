using Database;
using Domain.Entity;
using Domain.Enums;
using Domain.Services.Color;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Status
{
    public class ColorStatusService : IColorStatus
    {
        private readonly ApplicationContext _db;

        public ColorStatusService(ApplicationContext db)
        {
            _db = db;
        }

        public async Task<ColorStatusDto> TruckStatusAsync(string carNumber, CancellationToken token)
        {
            DateTime arrivalTime = DateTime.UtcNow;

            Trip trip = await _db.Trips
                .Include(t => t.Truck)
                .Include(t => t.Timeslot)
                .FirstOrDefaultAsync(x => x.Truck.Number == carNumber);

            if (trip.ArrivalTime < arrivalTime)
            {
                trip.Truck.ColorStatus = ColorStatus.BeLate;
                trip.Truck.Backlight = Backlights.Red;
            }

            if (DateTime.Parse(trip.Timeslot.To) <= arrivalTime + new TimeSpan(3, 5, 0) &&
                trip.Timeslot.Date.Day == arrivalTime.Day)
            {
                trip.Truck.ColorStatus = ColorStatus.NeedToSpeedUp;
                trip.Truck.Backlight = Backlights.Orange;
            }

            ColorStatusDto colorStatus = new ColorStatusDto()
            {
                Backlight = trip.Truck.Backlight,
                ColorStatus = trip.Truck.ColorStatus
            };

            _db.SaveChangesAsync(token);

            return colorStatus;
        }
    }
}
