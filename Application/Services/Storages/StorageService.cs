using AutoMapper;
using Database;
using Domain.Entity;
using Domain.Enums;
using Domain.Services.Storages;
using Domain.Services.Trips;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Application.Services.Storages
{
    public class StorageService : IStorageService
    {
        private readonly IMapper _mapper;
        private readonly ITripService _tripService;
        private readonly ApplicationContext _database;

        public StorageService(IMapper mapper, ApplicationContext database, ITripService tripService)
        {
            _mapper = mapper;
            _database = database;
            _tripService = tripService;
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
        public async Task<bool> CanDriveToStorageAsync(string carNumber, CancellationToken token)
        {
            DateTime arrivalTime = DateTime.UtcNow;

            Trip trip = await _database.Trips
                .Include(t => t.Truck)
                .FirstOrDefaultAsync(x => x.Truck.Number == carNumber);

            bool check = await _database.Trips.AnyAsync(x => x.Truck.Number == carNumber &&
            x.ArrivalTimePlan.AddMinutes(-30) <= arrivalTime &&
            arrivalTime <= x.ArrivalTimePlan.AddMinutes(30), token);

            if (check)
            {
                trip.Backlights = BacklightType.InWork.ToString();
            }

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

            var _trip = await _database.Trips.FirstOrDefaultAsync(x => x.Truck.Number == carNumber && x.ArrivalTimePlan.AddMinutes(-30) <= arrivalTime && arrivalTime <= x.ArrivalTimePlan.AddMinutes(30), token);
            if (_trip != null)
            {
                await _tripService.OccupancyAsync(_trip, token);
                return true;
            }

            return check;
        }

        public async Task<bool> CanDriveToStorageQRCodeAsync(IFormFile formFile, CancellationToken token)
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

            var trip = await _database.Trips.FirstOrDefaultAsync(x => x.QRCode == data && x.ArrivalTimePlan.AddMinutes(-30) <= arrivalTime && arrivalTime <= x.ArrivalTimePlan.AddMinutes(30), token);

            if (trip != null)
            {
                await _tripService.OccupancyAsync(trip, token);
                return true;
            }
            return false;
        }
    }
}
