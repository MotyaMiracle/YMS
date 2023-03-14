using AutoMapper;
using Domain.Entity;
using Domain.Services.Trucks;

namespace Yard_Management_System.AutoMapper
{
    public class MapTruck : Profile
    {
        public MapTruck()
        {
            CreateMap<Truck, TruckDto>()
                .ForPath(dest => dest.CompanyId.Value, opt => opt.MapFrom(src => src.CompanyId.ToString()))
                .ForMember(dto => dto.Id, opt => opt.MapFrom(src => src.Id.ToString()));

            CreateMap<TruckDto, Truck>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(src.Id) ? Guid.NewGuid() : Guid.Parse(src.Id)))
                .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => Guid.Parse(src.CompanyId.Value))); 
        }   
    }
}
