using Microsoft.AspNetCore.Http;

namespace Domain.Services.Files
{
    public interface IFileService
    {
        public Task<FileDto> GetAsync(Guid entityId, CancellationToken token);
        public Task<IEnumerable<FileDto>> GetAllAsync(CancellationToken token);
        public Task<FileDto> AddAsync(IFormFile formFile, Guid entityId, CancellationToken token );
    }
}
