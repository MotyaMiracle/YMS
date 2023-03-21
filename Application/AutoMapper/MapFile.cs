using AutoMapper;
using Domain.Entity;
using Domain.Services.Files;

namespace Application.AutoMapper
{
    public class MapFile : Profile
    {
        public MapFile()
        {
            CreateMap<EntityFile, FileDto>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(src => src.Id.ToString()));
            CreateMap<FileDto, EntityFile>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(src.Id) ? Guid.NewGuid() : Guid.Parse(src.Id)));
        }
    }
}
