using AutoMapper;
using Yard_Management_System;
using Yard_Management_System.Entity;

namespace Domain.Services.Trips
{
    public class TripService : ITripService
    {
        private readonly IMapper _mapper;
        private readonly ApplicationContext _db;

        public TripService(ApplicationContext db, IMapper mapper)
        {
            _mapper = mapper;
            _db = db;
        }
        public void Create(Guid entityId)
        {
            throw new NotImplementedException();
        }

        public async Task CreateAsync(TripDto trip, CancellationToken token)
        {
            trip.Id = Guid.NewGuid();
            trip.NowStatus = TripDto.Status.Create;
            Trip newTrip = _mapper.Map<Trip>(trip);
            await _db.Trips.AddAsync(newTrip, token);
            await _db.SaveChangesAsync(token);
        }
    }
}
