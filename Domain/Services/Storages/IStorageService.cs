using Domain.Entity;
using Microsoft.AspNetCore.Http;

namespace Domain.Services.Storages
{
    public interface IStorageService
    {
        Task<StorageDto> GetAsync(Guid storageId, CancellationToken token);

        Task CreateAndUpdateAsync(StorageDto storageDto, CancellationToken token);

        Task DeleteStorageAsync(Guid storageId, CancellationToken token);

        Task<IEnumerable<StorageDto>> GetAllAsync(CancellationToken token);

        Task<int> GetExcpectedOccupancy(DateTime selectedDate, string storageName, CancellationToken token);

        public Task<bool> CanDriveToStorageAsync(string carNumber, CancellationToken token);

        public Task<bool> CanDriveToStorageQRCodeAsync(IFormFile formFile, CancellationToken token);

    }
}
