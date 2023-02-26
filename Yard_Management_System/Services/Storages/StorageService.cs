using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Yard_Management_System.Entity;

namespace Yard_Management_System.Services.Storages
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
            if (storageDto.Id == new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"))
            {
                Storage storage = _mapper.Map<Storage>(storageDto);
                storage.Id = Guid.NewGuid();
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

            _database.Storages.Remove(storage);
            await _database.SaveChangesAsync(token);
        }

        public async Task<StorageDto> GetAsync(Guid storageId, CancellationToken token)
        {
            var storage = await _database.Storages
                .FirstOrDefaultAsync(s => s.Id == storageId, token);

            var response = _mapper.Map<StorageDto>(storage);

            return response;
        }

        public async Task<StorageEntriesDto> GetAllAsync(CancellationToken token)
        {
            var storages = await _database.Storages.ToListAsync(token);

            return new StorageEntriesDto { 
                Entries = storages.Select(s => new StorageDto 
                { 
                    Id = s.Id,
                    Name = s.Name,
                    Address = s.Address,
                    Latitude = s.Latitude,
                    Longitude = s.Longitude,
                    OpeningHours = s.OpeningHours,
                    DayOfWeeks = s.DayOfWeeks
                }).ToList()
            };

        }
    }
}
