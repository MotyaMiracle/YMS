﻿namespace Domain.Services.Trailers
{
    public interface ITrailerService
    {
        Task CreateAndUpdateAsync(TrailerDto trailerDto, CancellationToken token);
        Task<TrailerDto> GetAsync(Guid trailerId, CancellationToken token);
        Task<IEnumerable<TrailerDto>> GetAllAsync(CancellationToken token);
        Task DeleteAsync(Guid trailerId, CancellationToken token);
    }
}
