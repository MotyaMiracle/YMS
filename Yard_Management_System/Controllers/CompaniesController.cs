using Domain.Services.Companies;
using Domain.Services.Storages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Yard_Management_System.Controllers
{
    [Route("api/company")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompaniesController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> Get(CancellationToken token)
        {
            return Ok(await _companyService.GetAllAsync(token));
        }

        [HttpGet("getCompany")]
        public async Task<IActionResult> Get(Guid companyId, CancellationToken token)
        {
            return Ok(await _companyService.GetAsync(companyId, token));
        }

        [HttpPost("createCompany")]
        public async Task<IActionResult> Post(CompanyDto newCompany, CancellationToken token)
        {
            if (newCompany == null)
                return BadRequest();

            await _companyService.CreateAndUpdateAsync(newCompany, token);

            return Ok();
        }

        [HttpDelete("deleteCompany")]
        public async Task<IActionResult> Delete(Guid companyId, CancellationToken token)
        {
            if (companyId == Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"))
                return BadRequest();

            await _companyService.DeleteCompanyAsync(companyId, token);

            return Ok();
        }
    }
}
