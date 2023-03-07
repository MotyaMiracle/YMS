using AutoMapper;
using Database.Entity;
using Domain.Services.Storages;
using Microsoft.EntityFrameworkCore;
using Yard_Management_System;

namespace Domain.Services.Companies
{
    public class CompanyService : ICompanyService
    {
        private readonly IMapper _mapper;
        private readonly ApplicationContext _database;

        public CompanyService(IMapper mapper, ApplicationContext database)
        {
            _mapper = mapper;
            _database = database;
        }


        public async Task CreateAndUpdateAsync(CompanyDto companyDto, CancellationToken token)
        {
            //Create
            if (string.IsNullOrWhiteSpace(companyDto.Id))
            {
                Company company = _mapper.Map<Company>(companyDto);
                await _database.Companys.AddAsync(company, token);
            }
            //Update
            else
            {
                Company updateCompany = _mapper.Map<Company>(companyDto);
                _database.Companys.Update(updateCompany);
            }
            await _database.SaveChangesAsync(token);
        }

        public async Task DeleteCompanyAsync(Guid companyId, CancellationToken token)
        {
            Company company = await _database.Companys
                .FirstOrDefaultAsync(s => s.Id == companyId, token);

            _database.Companys.Remove(company);
            await _database.SaveChangesAsync(token);
        }

        public async Task<CompanyEntriesDto> GetAllAsync(CancellationToken token)
        {
            var companies = await _database.Companys.ToListAsync(token);

            return new CompanyEntriesDto
            {
                Entries = _mapper.Map<IEnumerable<CompanyDto>>(companies).ToList()
            };
        }

        public async Task<CompanyDto> GetAsync(Guid companyId, CancellationToken token)
        {
            var companies = await _database.Companys
                .FirstOrDefaultAsync(s => s.Id == companyId, token);

            var response = _mapper.Map<CompanyDto>(companies);

            return response;
        }
    }
}
