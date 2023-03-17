using AutoMapper;
using Database;
using Domain.Entity;
using Domain.Services.Timeslots;
using Domain.Services.Trucks;
using Microsoft.EntityFrameworkCore;
using static Application.Services.Timeslots.TimeslotService;

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

        public List<string> GetNotEmployedTimeslots()
        {
            var EmployedTimeslots = from t in _db.Trips orderby t.Timeslot != null select t.Timeslot;
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

            List<string> NotEmployedTimeslots = new List<string>();

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

        public async Task<TimeslotDto> CreateAsync(TimeslotDto timeslotDto, CancellationToken token)
        {
            Trip trip = await _db.Trips
                .FirstOrDefaultAsync(t => t.Id == Guid.Parse(timeslotDto.TripId.Value), token);

            Gate gate = await _db.Gates
                .FirstOrDefaultAsync(g => g.Id == trip.GateId, token);

            //Время необходимое для разгрузки/разгрузки паллет
            int WorkingTime = trip.PalletsCount * gate.PalletHandlingTime;

            var EmployedTimeslots = from t in _db.Trips orderby t.Timeslot != null select t.Timeslot;

            //Кол-во таймслотов для разгрузки/разгрузки паллет
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
