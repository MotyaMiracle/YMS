using AutoMapper;
using Domain.Services.Files;
using Domain.Services.Users;
using Yard_Management_System.Entity;

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
