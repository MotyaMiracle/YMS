using Microsoft.AspNetCore.Http;
using Yard_Management_System.Entity;

namespace Domain.Services.Files
{
    public interface IFileService
    {
        public Task<FileDto> GetAsync(Guid entityId, CancellationToken token);
        public Task<IEnumerable<FileDto>> GetAllAsync(CancellationToken token);
        public Task<FileDto> AddAsync(IFormFile formFile, Guid entityId, CancellationToken token );
    }
}
