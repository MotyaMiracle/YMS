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
                .ForPath(dest => dest.StorageId.Value, opt => opt.MapFrom(src => src.StorageId.ToString()));
            CreateMap<GateDto, Gate>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(src.Id) ? Guid.NewGuid() : Guid.Parse(src.Id)))
                .ForMember(dest => dest.StorageId, opt => opt.MapFrom(src => Guid.Parse(src.StorageId.Value)));
        }
    }
}
