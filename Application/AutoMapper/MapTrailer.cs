using AutoMapper;
using Domain.Entity;
using Domain.Services.Trailers;

namespace Yard_Management_System.AutoMapper
{
    public class MapTrailer : Profile
    {
        public MapTrailer()
        {
            CreateMap<Trailer, TrailerDto>()
                .ForPath(dest => dest.CompanyId.Value, opt => opt.MapFrom(src => src.CompanyId.ToString()))
                .ForMember(dto => dto.Id, opt => opt.MapFrom(src => src.Id.ToString()));

            CreateMap<TrailerDto, Trailer>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(src.Id) ? Guid.NewGuid() : Guid.Parse(src.Id)))
                .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => Guid.Parse(src.CompanyId.Value)));
        }
    }
}
