using AutoMapper;
using Domain.Services.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Yard_Management_System;
using Yard_Management_System.Entity;

namespace Domain.Services.Files
{
    public class FileService : IFileService
    {
        private readonly ApplicationContext _db;
        private readonly IMapper _mapper;

        public FileService(ApplicationContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<FileDto> AddAsync(IFormFile formFile, Guid entityId, CancellationToken token)
        {
            FileDto file = new FileDto { Id = Guid.NewGuid().ToString(), FileName = formFile.FileName, EntityId = entityId };

            using (var stream = new MemoryStream())
            {
                await formFile.CopyToAsync(stream);
                file.Data = stream.ToArray();
            }

            EntityFile newFile = _mapper.Map<EntityFile>(file);

            _db.Files.Add(newFile);
            _db.SaveChanges();
            return file;
        }

        public async Task<IEnumerable<FileDto>> GetAllAsync(CancellationToken token)
        {
            var files = await _db.Files
                .ToListAsync(token);

            return  _mapper.Map<IEnumerable<FileDto>>(files).ToList();
        }

        public async Task<FileDto> GetAsync(Guid fileId, CancellationToken token)
        {
            var file = await _db.Files
                .FirstOrDefaultAsync(f => f.Id == fileId, token);

            if (file is null)
                return null;

            var response = _mapper.Map<FileDto>(file);
            return response;
        }
    }
}
