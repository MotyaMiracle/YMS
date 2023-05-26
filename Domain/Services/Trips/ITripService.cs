using Domain.Services.Color;
﻿using Domain.Entity;

namespace Domain.Services.Trips
{
    public interface ITripService
    {
        Task CreateAsync (TripDto trip, CancellationToken token);
        Task<TripDto> GetAsync(Guid tripId, CancellationToken token);
        Task<BackligthDto> BackligthAsync(string entityId, CancellationToken token);
        Task OccupancyAsync(Trip trip,CancellationToken token);
        void Create(Guid entityId);
    }
}
