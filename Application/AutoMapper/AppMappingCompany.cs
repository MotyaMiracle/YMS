using AutoMapper;
using Domain.Entity;
using Domain.Services.Companies;
using System.Text.RegularExpressions;

namespace Application.AutoMapper
{
    public class AppMappingCompany : Profile
    {
        public AppMappingCompany()
        {
            CreateMap<Company, CompanyDto>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(src => src.Id.ToString()));
            CreateMap<CompanyDto, Company>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(src.Id) ? Guid.NewGuid() : Guid.Parse(src.Id)))
                .ForMember(dest => dest.Inn, opt => opt.MapFrom(src => Regex.IsMatch(src.Inn, "^(([0-9]{12})|([0-9]{10}))?$") ? src.Inn : "Неверно указан ИНН"));
        }
    }
}
