namespace Domain.Services.Companies
{
    public interface ICompanyService
    {
        Task<CompanyDto> GetAsync(Guid companyId, CancellationToken token);

        Task CreateAndUpdateAsync(CompanyDto companyDto, CancellationToken token);

        Task DeleteCompanyAsync(Guid companyId, CancellationToken token);

        Task<CompanyEntriesDto> GetAllAsync(CancellationToken token);
    }
}
