using AutoMapper;
using Database.Entity;
using Microsoft.EntityFrameworkCore;
using Yard_Management_System;

namespace Domain.Services.Gates
{
    public class GateService : IGatesService
    {
        private readonly ApplicationContext _database;
        private readonly IMapper _mapper;

        public GateService(ApplicationContext database, IMapper mapper)
        {
            _database = database;
            _mapper = mapper;
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

            _database.Gates.Remove(gate);
            await _database.SaveChangesAsync(token);
        }

        public async Task<GateEntriesDto> GetAllAsync(CancellationToken token)
        {
            var gates = await _database.Gates.ToListAsync(token);

            return new GateEntriesDto
            {
                Entries = _mapper.Map<IEnumerable<GateDto>>(gates).ToList()
            };
        }

        public async Task<GateDto> GetAsync(Guid gateId, CancellationToken token)
        {
            var gate = await _database.Gates
                .FirstOrDefaultAsync(s => s.Id == gateId, token);

            var response = _mapper.Map<GateDto>(gate);

            return response;
        }

        public async Task<bool> CanDriveToGateAsync(string carNumber, Guid tripId, CancellationToken token)
        {
            DateTime arrivalTime = DateTime.UtcNow;

            var trip = await _database.Trips
                .FirstOrDefaultAsync(s => s.Id == tripId, token);

            var storage = await _database.Storages
                .FirstOrDefaultAsync(s => s.Id == trip.StorageId, token);


            var truck = await _database.Trucks
                .FirstOrDefaultAsync(s => s.CarNumber == carNumber, token);


            var current = _database.Trips.Where(t => t.TruckId == truck.Id && t.StorageId == storage.Id);
            if (current == null)
            {
                return false;
            }

            if (arrivalTime <= trip.ArrivalTime.AddMinutes(+30) 
                && arrivalTime >= trip.ArrivalTime.AddMinutes(-30))
                return true;
            else
                return false;
        }
    }
}
