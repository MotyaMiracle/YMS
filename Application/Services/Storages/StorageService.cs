using AutoMapper;
using Database;
using Domain.Entity;
using Domain.Enums;
using Domain.Services.Storages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Application.Services.Storages
{
    public class StorageService : IStorageService
    {
        private readonly IMapper _mapper;
        private readonly ApplicationContext _database;

        public StorageService(IMapper mapper, ApplicationContext database)
        {
            _mapper = mapper;
            _database = database;
        }

        public async Task CreateAndUpdateAsync(StorageDto storageDto, CancellationToken token)
        {
            //Create
            if (string.IsNullOrWhiteSpace(storageDto.Id))
            {
                Storage storage = _mapper.Map<Storage>(storageDto);
                await _database.Storages.AddAsync(storage, token);
            }
            //Update
            else
            {
                Storage updateStorage = _mapper.Map<Storage>(storageDto);
                _database.Storages.Update(updateStorage);
            }
            await _database.SaveChangesAsync(token);
        }

        public async Task DeleteStorageAsync(Guid storageId, CancellationToken token)
        {
            Storage storage = await _database.Storages
                .FirstOrDefaultAsync(s => s.Id == storageId, token);

            if (storage is null)
                return;

            _database.Storages.Remove(storage);
            await _database.SaveChangesAsync(token);
        }

        public async Task<StorageDto> GetAsync(Guid storageId, CancellationToken token)
        {
            var storage = await _database.Storages
                .FirstOrDefaultAsync(s => s.Id == storageId, token);

            if (storage is null)
                return null;

            var response = _mapper.Map<StorageDto>(storage);

            return response;
        }

        public async Task<IEnumerable<StorageDto>> GetAllAsync(CancellationToken token)
        {
            var storages = await _database.Storages.ToListAsync(token);

            return _mapper.Map<IEnumerable<StorageDto>>(storages).ToList();
        }

        public async Task<int> GetExcpectedOccupancy(DateTime selectedDate, string storageName, CancellationToken token)
        {
            if (string.IsNullOrWhiteSpace(storageName))
            {
                var trips = await _database.Trips
                    .Include(s => s.Storage)
                    .Include(t => t.Timeslot)
                    .Where(t => t.ArrivalTimePlan.Date == selectedDate)
                    .ToListAsync(token);

                return await CalculateExpectedOccupancy(trips);
            }
            else
            {
                var trips = await _database.Trips
                                    .Include(s => s.Storage)
                                    .Include(l => l.Timeslot)
                                    .Where(t => t.ArrivalTimePlan.Date == selectedDate && t.Storage.Name == storageName)
                                    .ToListAsync(token);

                return await CalculateExpectedOccupancy(trips);
            }
        }

        private async Task<int> CalculateExpectedOccupancy(List<Trip> trips)
        {
            var expectedOccupancy = 0;

            foreach(var trip in trips)
            {
                switch (trip.Timeslot.Status)
                {
                    case OperationType.Loading:
                        expectedOccupancy -= trip.PalletsCount;
                        break;

                    case OperationType.Unloading:
                        expectedOccupancy += trip.PalletsCount;
                        break;
                }
            }

            return expectedOccupancy;
        }
    }
}
