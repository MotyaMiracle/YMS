using AutoMapper;
using Domain.Services.History;
using Domain.Services.Users;
using Yard_Management_System;
using Yard_Management_System.Entity;

namespace Domain.Services.Trips
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
            trip.NowStatus = TripDto.Status.Create;
            Trip newTrip = _mapper.Map<Trip>(trip);
            await _historyService.SaveAsync(newTrip.Id, "Создана путёвка", await _userProvider.GetCurrentUserAsync(token), token);
            await _db.Trips.AddAsync(newTrip, token);
            await _db.SaveChangesAsync(token);
        }
    }
}
