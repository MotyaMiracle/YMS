using AutoMapper;
using Domain.Entity;
using Domain.Services.Drivers;

namespace Yard_Management_System.AutoMapper
{
    public class AppMappingDriver : Profile
    {
        public AppMappingDriver()
        {
            CreateMap<Driver, DriverDto>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(src => src.Id.ToString()));
            CreateMap<DriverDto, Driver>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(src.Id) ? Guid.NewGuid() : Guid.Parse(src.Id)));
        }
    }
}
