using Yard_Management_System.Entity;

namespace Domain.Services.Storages
{
    public interface IStorageService
    {
        Task<StorageDto> GetAsync(Guid storageId, CancellationToken token);

        Task CreateAndUpdateAsync(StorageDto storageDto, CancellationToken token);

        Task DeleteStorageAsync(Guid storageId, CancellationToken token);

        Task<StorageEntriesDto> GetAllAsync(CancellationToken token);

    }
}
