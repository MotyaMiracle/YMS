using AutoMapper;
using Database;
using Domain.Entity;
using Domain.Enums;
using Domain.Services.Gates;
using Domain.Services.Trips;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Gates
{
    public class GateService : IGatesService
    {
        private readonly ApplicationContext _database;
        private readonly ITripService _tripService;
        private readonly IMapper _mapper;

        public GateService(ApplicationContext database, IMapper mapper, ITripService tripService)
        {
            _database = database;
            _mapper = mapper;
            _tripService = tripService;
        }

        public async Task CreateAndUpdateAsync(GateDto gateDto, CancellationToken token)
        {
            //Create
            if (string.IsNullOrWhiteSpace(gateDto.Id))
            {
                Gate gate = _mapper.Map<Gate>(gateDto);
                await _database.Gates.AddAsync(gate,token);
            }
            //Update
            else
            {
                Gate updateGates = _mapper.Map<Gate>(gateDto);
                _database.Gates.Update(updateGates);
            }
            await _database.SaveChangesAsync(token);
        }

        public async Task DeleteGateAsync(Guid gateId, CancellationToken token)
        {
            Gate gate = await _database.Gates
                .FirstOrDefaultAsync(s => s.Id == gateId, token);

            if (gate is null)
                return;

            _database.Gates.Remove(gate);
            await _database.SaveChangesAsync(token);
        }

        public async Task<IEnumerable<GateDto>> GetAllAsync(CancellationToken token)
        {
            var gates = await _database.Gates.ToListAsync(token);

            return _mapper.Map<IEnumerable<GateDto>>(gates).ToList();
        }

        public async Task<GateDto> GetAsync(Guid gateId, CancellationToken token)
        {
            var gate = await _database.Gates
                .FirstOrDefaultAsync(s => s.Id == gateId, token);

            if (gate is null)
                return null;

            var response = _mapper.Map<GateDto>(gate);

            return response;
        }

        public async Task<bool> CanDriveToGateAsync(string carNumber, CancellationToken token)
        {
            DateTime arrivalTime = DateTime.UtcNow;

            Trip trip = await _database.Trips.FirstOrDefaultAsync(
                x => x.Truck.Number == carNumber, token);

            bool check = await _database.Trips.AnyAsync(x => x.Truck.Number == carNumber &&
            x.ArrivalTimePlan.AddMinutes(-30) <= arrivalTime &&
            arrivalTime <= x.ArrivalTimePlan.AddMinutes(30),
            token);

            if (trip.NowStatus == TripStatus.ArriveAtStorage)
            {
                trip.NowStatus = TripStatus.Left;
                await _database.SaveChangesAsync(token);
                return true;
            }

            if (check && trip.NowStatus != TripStatus.Left)
            {
                trip.ArrivalTimeFact = arrivalTime;
                trip.NowStatus = TripStatus.ArriveAtStorage;
            }

            await _database.SaveChangesAsync(token);
            
            var trip = await _database.Trips.FirstOrDefaultAsync(x => x.Truck.Number == carNumber && x.ArrivalTime.AddMinutes(-30) <= arrivalTime && arrivalTime <= x.ArrivalTime.AddMinutes(30), token);
            if (trip != null)
            {
                await _tripService.OccupancyAsync(trip, token);
                return true;
            }
            return check;
        }

        public async Task<bool> CanDriveToGateQRCodeAsync(IFormFile formFile, CancellationToken token)
        {
            if (formFile is null)
                return false;

            byte[] data;
            DateTime arrivalTime = DateTime.UtcNow;

            using (var stream = new MemoryStream())
            {
                await formFile.CopyToAsync(stream);
                data = stream.ToArray();
            }

            var trip = await _database.Trips.FirstOrDefaultAsync(x => x.QRCode == data && x.ArrivalTime.AddMinutes(-30) <= arrivalTime && arrivalTime <= x.ArrivalTime.AddMinutes(30), token);

            if (trip != null)
            {
                await _tripService.OccupancyAsync(trip, token);
                return true;
            }
            return false;
        }

    }
}
