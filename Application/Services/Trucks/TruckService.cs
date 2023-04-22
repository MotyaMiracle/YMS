using AutoMapper;
using Database;
using Domain.Entity;
using Domain.Enums;
using Domain.Services.Trucks;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Trucks
{
    public class TruckService : ITruckService
    {
        private readonly ApplicationContext _db;
        private readonly IMapper _mapper;

        public TruckService(ApplicationContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task CreateAndUpdateAsync(TruckDto truckDto, CancellationToken token)
        {
            //Create
            if (string.IsNullOrWhiteSpace(truckDto.Id))
            {
                Truck truck = _mapper.Map<Truck>(truckDto);
                //truck.CompanyId = Guid.NewGuid();
                await _db.Trucks.AddAsync(truck, token);
            }
            //Update
            else
            {
                Truck updateTruck = _mapper.Map<Truck>(truckDto);
                _db.Trucks.Update(updateTruck);
            }
            await _db.SaveChangesAsync(token);
        }

        public async Task DeleteAsync(Guid truckId, CancellationToken token)
        {
            Truck truck = await _db.Trucks
                .FirstOrDefaultAsync(d => d.Id == truckId, token);

            if (truck is null)
                return;

            _db.Trucks.Remove(truck);
            await _db.SaveChangesAsync(token);
        }

        public async Task<IEnumerable<TruckDto>> GetAllAsync(CancellationToken token)
        {
            var trucks = await _db.Trucks.ToListAsync(token);

            return _mapper.Map<IEnumerable<TruckDto>>(trucks).ToList();
        }

        public async Task<TruckDto> GetAsync(Guid truckId, CancellationToken token)
        {
            var truck = await _db.Trucks
                .FirstOrDefaultAsync(s => s.Id == truckId, token);

            if (truck is null)
                return null;

            var response = _mapper.Map<TruckDto>(truck);

            return response;
        }

        public async Task<ColorStatus?> TruckStatusAsync(string carNumber, CancellationToken token)
        {
            DateTime arrivalTime = DateTime.UtcNow;

            Trip trip = await _db.Trips
                .Include(t => t.Truck)
                .Include(t => t.Timeslot)
                .FirstOrDefaultAsync(x => x.Truck.Number == carNumber);

            if (trip.ArrivalTime < arrivalTime)
                trip.Truck.ColorStatus = ColorStatus.BeLate;

            if (DateTime.Parse(trip.Timeslot.To) <= arrivalTime + new TimeSpan(3, 5, 0) &&
                trip.Timeslot.Date.Day == arrivalTime.Day)
            {
                trip.Truck.ColorStatus = ColorStatus.NeedToSpeedUp;
            }

            _db.SaveChangesAsync(token);

            return trip.Truck.ColorStatus;
        }
    }
}
