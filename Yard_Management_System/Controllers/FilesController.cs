using AutoMapper;
using Domain.Services.Files;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Yard_Management_System.Entity;

namespace Yard_Management_System.Controllers
{
    [Authorize]
    [Route("api/files")]
    [ApiController]
    public class FilesController : Controller
    {
        ApplicationContext _db;
        IFileService _fileService;

        public FilesController(ApplicationContext db, IFileService fileService)
        {
            _db = db;
            _fileService = fileService;
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile formFile,Guid entityId, CancellationToken token)
        {
            return Ok(await _fileService.AddAsync(formFile, entityId, token));
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll(CancellationToken token)
        {
            return Ok(await _fileService.GetAllAsync(token));
        }

        [HttpGet]
        public async Task<IActionResult> Get(Guid fileId, CancellationToken token)
        {
            return Ok(await _fileService.GetAsync(fileId, token));
        }
    }
}
