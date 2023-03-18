using AutoMapper;
using Database;
using Domain.Entity;
using Domain.Services.Timeslots;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        public List<string> GetNotEmployedTimeslots(DateTime date, string gateName)
        {
            var EmployedTimeslots = _db.Trips
                .Where(t => t.Timeslot != null)
                .Where(d => d.Timeslot.Date.Day == date.Day && d.Timeslot.Date.Month == date.Month)
                .Where(g => g.Gate.Name == gateName)
                .Select(t => t.Timeslot);

            List<string> NotEmployedTimeslots = new List<string>();

            int count = 0;

            //Добавление всех таймслотов
            var Timeslots = new List<TimeSpan>();
            for (int i = 0; i < 48; i++)
            {
                Timeslots.Add(new TimeSpan(count, 0, 0));
                i++;
                Timeslots.Add(new TimeSpan(count, 30, 0));
                count++;
            }
            count = 1;

            //Из таймслотов убираем занятые таймслоты
            foreach (var t in EmployedTimeslots)
            {
                foreach (var x in Timeslots.ToList())
                {
                    if (DateTime.Parse(t.From).ToShortTimeString() == DateTime.Parse(x.ToString()).ToShortTimeString() ||
                        DateTime.Parse(t.From) <= DateTime.Parse(x.ToString()) && DateTime.Parse(t.To) > DateTime.Parse(x.ToString()))
                    {
                        Timeslots.Remove(x); 
                    }        
                }    
            }

            //Приведение таймслотов в читабельный вид
            foreach (var t in Timeslots)
            {
                if (count == Timeslots.Count() && t == new TimeSpan(23, 30, 0))
                {
                    NotEmployedTimeslots.Add($"{t} - 23:59:00");
                    return NotEmployedTimeslots;
                }
                NotEmployedTimeslots.Add($"{t} - {t + new TimeSpan(0, 30, 0)}");
                count++;
            }
            return NotEmployedTimeslots;
        }

        public async Task<TimeslotDto> CreateAsync(TimeslotDto timeslotDto, string gateName, CancellationToken token)
        {
            //Время необходимое для разгрузки/погрузки паллет
            Trip trip = await _db.Trips
                .FirstOrDefaultAsync(t => t.Id == Guid.Parse(timeslotDto.TripId.Value), token);

            Gate gate = await _db.Gates
                .FirstOrDefaultAsync(g => g.Id == trip.GateId, token);

            int WorkingTime = trip.PalletsCount * gate.PalletHandlingTime;

            var EmployedTimeslots = _db.Trips
            .Where(t => t.Timeslot != null)
            .Where(d => d.Timeslot.Date.Day == timeslotDto.Date.Day && d.Timeslot.Date.Month == timeslotDto.Date.Month)
                .Where(g => g.Gate.Name == gateName)
                .Select(t => t.Timeslot);

            //Кол-во таймслотов для разгрузки/погрузки паллет
            int CountOfTimeslots = (int)Math.Ceiling((double)WorkingTime / 30);

            //Проверка правильно ли выбрано кол - во таймслотов(не больше и не меньше необходимого)
            if (DateTime.Parse(timeslotDto.From).AddMinutes(30 * CountOfTimeslots).ToShortTimeString() != DateTime.Parse(timeslotDto.To).ToShortTimeString())
            {
                return null;
            }

            //Проверка не пересекается ли любой из выбранных таймслотов с уже забронируемыми таймслотами
            foreach (var t in EmployedTimeslots)
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