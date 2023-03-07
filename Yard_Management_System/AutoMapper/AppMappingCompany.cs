using AutoMapper;
using Database.Entity;
using Domain.Services.Companies;

namespace Yard_Management_System.AutoMapper
{
    public class AppMappingCompany : Profile
    {
        public AppMappingCompany() 
        {
            CreateMap<Company, CompanyDto>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dto => dto.TruckId, opt => opt.MapFrom(src => src.TruckId.ToString()))
                .ForMember(dto => dto.TrailerId, opt => opt.MapFrom(src => src.TrailerId.ToString()));
            CreateMap<CompanyDto, Company>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(src.Id) ? Guid.NewGuid() : Guid.Parse(src.Id)));
        }
    }
}
