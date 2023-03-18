using AutoMapper;
using Database;
using Domain.Entity;
using Domain.Enums;
using Domain.Services.History;
using Domain.Services.Trips;
using Domain.Services.Users;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Trips
{
    public class TripService : ITripService
    {
        private readonly IMapper _mapper;
        private readonly IHistoryService _historyService;
        private readonly IUserProvider _userProvider;
        private readonly ApplicationContext _db;

        public TripService(ApplicationContext db, IMapper mapper, IHistoryService historyService, IUserProvider userProvider)
        {
            _mapper = mapper;
            _db = db;
            _historyService = historyService;
            _userProvider = userProvider;
        }
        public void Create(Guid entityId)
        {
            throw new NotImplementedException();
        }

        public async Task CreateAsync(TripDto trip, CancellationToken token)
        {
            trip.NowStatus = TripStatus.Create;
            Trip newTrip = _mapper.Map<Trip>(trip);
            await _historyService.SaveAsync(newTrip.Id, "Создана путёвка", await _userProvider.GetCurrentUserAsync(token), token);
            await _db.Trips.AddAsync(newTrip, token);
            await _db.SaveChangesAsync(token);
        }

        public async Task OperationAsync(Guid tripId, CancellationToken token)
        {
            var trip = await _db.Trips.FirstOrDefaultAsync(t => t.Id == tripId, token);

            if (trip is null)
                return;

            var storage = await _db.Storages.FirstOrDefaultAsync(s => s.Id == trip.StorageId, token);

            var timeslot = await _db.Timeslots.FirstOrDefaultAsync(t => t.Id == trip.TimeslotId, token);

            if (storage is null && timeslot is null)
                return;


            switch (timeslot.Status)
            {
                case GateStatus.Loading:
                    storage.OccupancyExpected = storage.OccupancyActual - trip.PalletsCount;
                    if(storage.OccupancyExpected >= 0)
                    {
                        storage.OccupancyActual = storage.OccupancyExpected;
                    }
                    else
                    {
                        storage.OccupancyActual = 0;
                    }
                    break;
                case GateStatus.Unloading:
                    storage.OccupancyExpected = storage.OccupancyActual + trip.PalletsCount;
                    if(storage.OccupancyExpected <= storage.Capacity)
                    {
                        storage.OccupancyActual = storage.OccupancyExpected;
                    }
                    else
                    {
                        var temp = Math.Abs(trip.PalletsCount - storage.Capacity);
                        trip.PalletsCount -= temp;
                        storage.OccupancyActual += temp;
                    }
                    break;
            }

            await _db.SaveChangesAsync(token);
        }
    }
}
