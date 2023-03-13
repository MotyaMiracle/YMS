using AutoMapper;
using Domain.Services.Storages;
using Database.Entity;

namespace Yard_Management_System.AutoMapper
{
    public class AppMappingStorage : Profile
    {
        public AppMappingStorage()
        {
            CreateMap<Storage, StorageDto>()
               .ForMember(dto => dto.Id, opt => opt.MapFrom(src => src.Id.ToString()));
            CreateMap<StorageDto, Storage>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(src.Id) ? Guid.NewGuid() : Guid.Parse(src.Id)));
        }
    }
}
