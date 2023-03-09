using AutoMapper;
using Yard_Management_System;
using Database.Entity;
using Microsoft.EntityFrameworkCore;

namespace Domain.Services.Trailers
{
    public class TrailerService : ITrailerService
    {
        private readonly ApplicationContext _db;
        private readonly IMapper _mapper;

        public TrailerService(ApplicationContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task CreateAndUpdateAsync(TrailerDto trailerDto, CancellationToken token)
        {
            //Create
            if (string.IsNullOrWhiteSpace(trailerDto.Id))
            {
                Trailer trailer = _mapper.Map<Trailer>(trailerDto);
                await _db.Trailers.AddAsync(trailer, token);
            }
            //Update
            else
            {
                Trailer updateTrailer = _mapper.Map<Trailer>(trailerDto);
                _db.Trailers.Update(updateTrailer);
            }
            await _db.SaveChangesAsync(token);
        }

        public async Task DeleteAsync(Guid trailerId, CancellationToken token)
        {
            Trailer trailer = await _db.Trailers
                .FirstOrDefaultAsync(d => d.Id == trailerId, token);

            _db.Trailers.Remove(trailer);
            await _db.SaveChangesAsync(token);
        }

        public async Task<IEnumerable<TrailerDto>> GetAllAsync(CancellationToken token)
        {
            var trailers = await _db.Trailers.ToListAsync(token);

            return _mapper.Map<IEnumerable<TrailerDto>>(trailers).ToList();
        }

        public async Task<TrailerDto> GetAsync(Guid trailerId, CancellationToken token)
        {
            var trailer = await _db.Trailers
                .FirstOrDefaultAsync(s => s.Id == trailerId, token);

            var response = _mapper.Map<TrailerDto>(trailer);

            return response;
        }
    }    
}   
