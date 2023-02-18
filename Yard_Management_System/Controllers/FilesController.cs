using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Yard_Management_System.Entity;

namespace Yard_Management_System.Controllers
{
    [Route("api/files")]
    [ApiController]
    public class FilesController : Controller
    {
        ApplicationContext db;

        public FilesController(ApplicationContext context)
        {
            db = context;
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile formFile, CancellationToken token)
        {
            MyFile file = new MyFile { Id = Guid.NewGuid(), FileName = formFile.FileName, DriverId = new Guid("5064330d-daf1-48bb-9b60-59ea2c1db0ba") };
            using (var stream = new MemoryStream())
            {
                await formFile.CopyToAsync(stream);

                file.Data = stream.ToArray();
            }
            db.Files.Add(file);
            db.SaveChanges();
            return Ok(file);
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken token)
        {
            var files = await db.Files.ToListAsync(token);
            return Ok(files);
        }
    }
}
