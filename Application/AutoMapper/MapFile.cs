using AutoMapper;
using Domain.Entity;
using Domain.Services.Files;

namespace Yard_Management_System.AutoMapper
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
