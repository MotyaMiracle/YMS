using AutoMapper;
using Database.Entity;
using Domain.Services.Gates;

namespace Yard_Management_System.AutoMapper
{
    public class AppMappingGate : Profile
    {
        public AppMappingGate() 
        {
            CreateMap<Gate, GateDto>()
               .ForMember(dto => dto.Id, opt => opt.MapFrom(src => src.Id.ToString()))
               .ForMember(dto => dto.PalletHandlingTime, opt => opt.MapFrom(src => src.PalletHandlingTime.ToShortTimeString()))
               .ForMember(dto => dto.OpeningHour, opt => opt.MapFrom(src => src.OpeningHour.ToShortTimeString()))
               .ForMember(dto => dto.ClosingHour, opt => opt.MapFrom(src => src.ClosingHour.ToShortTimeString()));
            CreateMap<GateDto, Gate>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(src.Id) ? Guid.NewGuid() : Guid.Parse(src.Id)))
                .ForMember(dest => dest.PalletHandlingTime, opt => opt.MapFrom(src => TimeOnly.Parse(src.PalletHandlingTime)))
                .ForMember(dest => dest.OpeningHour, opt => opt.MapFrom(src => TimeOnly.Parse(src.OpeningHour)))
                .ForMember(dest => dest.ClosingHour, opt => opt.MapFrom(src => TimeOnly.Parse(src.ClosingHour)));
        }
    }
}
