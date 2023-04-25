using AutoMapper;
using Database;
using Domain.Entity;
using Domain.Services.Timeslots;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Timeslots
{  
    public class TimeslotService : ITimeslotService
    {
        TimeSpan OneDay = new TimeSpan(1, 0, 0, 0);
        private readonly ApplicationContext _db;
        private readonly IMapper _mapper;

        public TimeslotService(ApplicationContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<EntryTimeslotView> GetTimeslotsAsync(Guid tripId, DateTime selectedDate, CancellationToken token)
        {
            Trip trip = await _db.Trips.Include(x => x.Gate)
                .FirstOrDefaultAsync(t => t.Id == tripId, token);

            var gates = await _db.Gates
                .Where(s => s.StorageId == trip.StorageId)
                .Select(g => g.Name).ToListAsync();

            const int maxTimeslotPerDay = 47;

            var allTimeslots = new List<List<TimeSpan>>();

            var timeslots = new List<TimeSpan>();

            int workingTime = trip.PalletsCount * trip.Gate.PalletHandlingTime;

            int time = (int)Math.Ceiling((double)workingTime / 60) * 60;

            //Проверка на правильно ,указанную дату, +- 1 день от запланированного приезда тс
            if (trip.ArrivalTime.Date != selectedDate.Date &&
                (trip.ArrivalTime.Date + OneDay) != selectedDate.Date &&
                (trip.ArrivalTime.Date - OneDay) != selectedDate.Date)
                return null;

            Dictionary<string, List<Timeslot>> timeslotsByGates = await _db.Trips
                .Include(t => t.Timeslot)
                .Where(t => t.Timeslot != null)
                .Where(d => d.Timeslot.Date.Date == trip.ArrivalTime.Date ||
                 d.Timeslot.Date.Date == trip.ArrivalTime.Date + OneDay ||
                 d.Timeslot.Date.Date == trip.ArrivalTime.Date - OneDay)
                .Where(g => g.StorageId == trip.StorageId)
                .OrderBy(g => g.GateId)
                .GroupBy(g => g.Gate.Name)
                .ToDictionaryAsync(g => g.Key, t => t.Select(t=> t.Timeslot).ToList());

            int minutes = 0;
            //Добавление всех таймслотов
            for (int i = 0; i < maxTimeslotPerDay; i++)
            {
                timeslots.Add(new TimeSpan(0, minutes, 0));
                if (timeslots[i] + new TimeSpan(0, time, 0) >= OneDay)
                {
                    break;
                }
                minutes += 30;
            }

            //Добавление таймслотов на каждые ворота
            for (int g = 0; g < gates.Count(); g++)
            {
                allTimeslots.Add(timeslots.ToList());
            }

            //Из таймслотов убираем занятые таймслоты
            for (int g = 0; g < gates.Count(); g++)
            {
                int count = 0;
                foreach (var t in timeslotsByGates)
                {

                    foreach (var x in timeslots.ToList())
                    {
                        if ((gates[g] == t.Key && t.Value[count].Date.Day == selectedDate.Day && (DateTime.Parse(t.Value[count].From).ToShortTimeString() == DateTime.Parse(x.ToString()).ToShortTimeString() ||
                            DateTime.Parse(t.Value[count].From) <= DateTime.Parse(x.ToString()) && DateTime.Parse(t.Value[count].To) > DateTime.Parse(x.ToString()) ||
                            DateTime.Parse(t.Value[count].From) < DateTime.Parse(x.ToString()).AddMinutes(time) && DateTime.Parse(t.Value[count].To) >= DateTime.Parse(x.ToString()).AddMinutes(time))) ||
                            (DateTime.Parse(x.ToString()).AddMinutes(time).Date > DateTime.Parse(x.ToString()).Date &&
                            x + new TimeSpan(0, time, 0) != OneDay))

                        {
                            allTimeslots[g].Remove(x);
                        }
                    }
                    count++;
                }
            }

            var resultTimeslots = new List<TimeSpan>();

            foreach (var t in allTimeslots)
            {
                resultTimeslots = t.Union(t).ToList();
            }

            return new EntryTimeslotView
            {
                Entries = resultTimeslots.Select(x => new TimeslotViewDto
                {
                    AllGates = gates,
                    From = DateTime.Parse(x.ToString()).ToShortTimeString(),
                    To = DateTime.Parse(x.ToString()).AddMinutes(time).ToShortTimeString(),
                    Date = selectedDate,
                }).ToList()
            };
        }

        public async Task<TimeslotDto> CreateAsync(TimeslotDto timeslotDto, Guid tripId, CancellationToken token)
        {
            Trip trip = await _db.Trips
                .FirstOrDefaultAsync(t => t.Id == tripId, token);

            Dictionary<string, List<Timeslot>> timeslotsByGates = await _db.Trips
                .Include(t => t.Timeslot)
                .Where(t => t.Timeslot != null)
                .Where(d => d.Timeslot.Date.Date == trip.ArrivalTime.Date ||
                 d.Timeslot.Date.Date == trip.ArrivalTime.Date + OneDay ||
                 d.Timeslot.Date.Date == trip.ArrivalTime.Date - OneDay)
                .Where(g => g.StorageId == trip.StorageId)
                .OrderBy(g => g.GateId)
                .GroupBy(g => g.Gate.Name)
                .ToDictionaryAsync(g => g.Key, t => t.Select(t => t.Timeslot).ToList());

            if (timeslotsByGates.Any(g => g.Value.Any(t => DateTime.Parse(t.From) <= DateTime.Parse(timeslotDto.From) && DateTime.Parse(t.To) > DateTime.Parse(timeslotDto.From) ||
                        DateTime.Parse(t.From) < DateTime.Parse(timeslotDto.To) && DateTime.Parse(t.To) >= DateTime.Parse(timeslotDto.To))))
            {
                return null;
            }

            Timeslot timeslot = _mapper.Map<Timeslot>(timeslotDto);
            timeslot.TripId = tripId;
            await _db.Timeslots.AddAsync(timeslot, token);
            await _db.SaveChangesAsync(token);

            return timeslotDto;
        }
    }
}