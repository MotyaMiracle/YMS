using AutoMapper;
using Database;
using Domain.Entity;
using Domain.Services.Timeslots;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Timeslots
{
    public class TimeslotService : ITimeslotService
    {
        private readonly ApplicationContext _db;
        private readonly IMapper _mapper;

        public TimeslotService(ApplicationContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<EntryTimeslotView> GetTimeslotsAsync(Guid tripId, DateTime selectedDate, CancellationToken token)
        {
            Trip trip = await _db.Trips
                .FirstOrDefaultAsync(t => t.Id == tripId, token);

            Gate gate = await _db.Gates
                .FirstOrDefaultAsync(g => g.Id == trip.GateId, token);

            List<string> notEmployedTimeslots = new List<string>();

            const int maxTimeslotPerDay = 47;

            var timeslots = new List<TimeSpan>();

            int minutes = 0;

            int workingTime = trip.PalletsCount * gate.PalletHandlingTime;

            int time = (int)Math.Ceiling((double)workingTime / 30) * 30;

            //Проверка на правильно ,указанную дату, +- 1 день от запланированного приезда тс
            if (trip.ArrivalTime.Date != selectedDate.Date &&
                (trip.ArrivalTime.Date + new TimeSpan(1, 0, 0, 0)) != selectedDate.Date &&
                (trip.ArrivalTime.Date - new TimeSpan(1, 0, 0, 0)) != selectedDate.Date)
                return null;

            var employedTimeslots = _db.Trips
                .Where(t => t.Timeslot != null)
                .Where(d => d.Timeslot.Date.Date == trip.ArrivalTime.Date ||
                 d.Timeslot.Date.Date == trip.ArrivalTime.Date + new TimeSpan(1, 0, 0, 0) ||
                 d.Timeslot.Date.Date == trip.ArrivalTime.Date - new TimeSpan(1, 0, 0, 0))
                .Where(g => g.StorageId == trip.StorageId)
                .Select(t => t.Timeslot);

            //Добавление всех таймслотов
            for (int i = 0; i < maxTimeslotPerDay; i++)
            {
                timeslots.Add(new TimeSpan(0, minutes, 0));
                minutes += time;
                if (minutes >= 1440)
                {
                    break;
                }
            }

            //Из таймслотов убираем занятые таймслоты
            foreach (var t in employedTimeslots)
            {
                foreach (var x in timeslots.ToList())
                {
                    if (t.Date.Day == selectedDate.Day && (DateTime.Parse(t.From).ToShortTimeString() == DateTime.Parse(x.ToString()).ToShortTimeString() ||
                        DateTime.Parse(t.From) <= DateTime.Parse(x.ToString()) && DateTime.Parse(t.To) > DateTime.Parse(x.ToString()) ||
                        DateTime.Parse(t.From) < DateTime.Parse(x.ToString()).AddMinutes(time) && DateTime.Parse(t.To) >= DateTime.Parse(x.ToString()).AddMinutes(time) ||
                        (DateTime.Parse(x.ToString()).AddMinutes(time).Day >= DateTime.Parse(x.ToString()).Day + 1 && 
                        x + new TimeSpan(0, time, 0) != new TimeSpan(1,0,0,0))))

                    {
                        timeslots.Remove(x); 
                    }        
                }    
            }

            return new EntryTimeslotView
            {
                Entries = timeslots.Select(x => new TimeslotViewDto
                {
                    From = DateTime.Parse(x.ToString()).ToShortTimeString(),
                    To = DateTime.Parse(x.ToString()).AddMinutes(time).ToShortTimeString(),
                    Date = selectedDate,
                }).ToList()
            };
        }

        public async Task<TimeslotDto> CreateAsync(TimeslotDto timeslotDto, string gateName, CancellationToken token)
        {            
            Trip trip = await _db.Trips
                .FirstOrDefaultAsync(t => t.Id == Guid.Parse(timeslotDto.TripId.Value), token);

            Gate gate = await _db.Gates
                .FirstOrDefaultAsync(g => g.Id == trip.GateId, token);
            
            //Время необходимое для разгрузки/погрузки паллет
            int workingTime = trip.PalletsCount * gate.PalletHandlingTime;

            var employedTimeslots = _db.Trips
            .Where(t => t.Timeslot != null)
            .Where(d => d.Timeslot.Date.Day == timeslotDto.Date.Day && d.Timeslot.Date.Month == timeslotDto.Date.Month)
                .Where(g => g.Gate.Name == gateName)
                .Select(t => t.Timeslot);

            //Кол-во таймслотов для разгрузки/погрузки паллет
            int countOfTimeslots = (int)Math.Ceiling((double)workingTime / 30);

            //Проверка правильно ли выбрано кол - во таймслотов(не больше и не меньше необходимого)
            if (DateTime.Parse(timeslotDto.From).AddMinutes(30 * countOfTimeslots).ToShortTimeString() != DateTime.Parse(timeslotDto.To).ToShortTimeString())
            {
                return null;
            }

            //Проверка не пересекается ли любой из выбранных таймслотов с уже забронируемыми таймслотами
            foreach (var t in employedTimeslots)
            {
                if (t != null)
                {
                    if (DateTime.Parse(t.From) <= DateTime.Parse(timeslotDto.From) && DateTime.Parse(t.To) > DateTime.Parse(timeslotDto.From) ||
                        DateTime.Parse(t.From) < DateTime.Parse(timeslotDto.To) && DateTime.Parse(t.To) >= DateTime.Parse(timeslotDto.To))
                    {
                        return null;
                    }
                }
            }

            Timeslot timeslot = _mapper.Map<Timeslot>(timeslotDto);
            await _db.Timeslots.AddAsync(timeslot, token);
            await _db.SaveChangesAsync(token);

            return timeslotDto;
        }
    }
}