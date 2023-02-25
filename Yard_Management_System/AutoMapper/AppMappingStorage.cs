using AutoMapper;
using Yard_Management_System.Services.Storages;
using Yard_Management_System.Entity;

namespace Yard_Management_System.AutoMapper
{
    public class AppMappingStorage : Profile
    {
        public AppMappingStorage()
        {
            CreateMap<Storage, StorageDto>().ReverseMap();
        }
    }
}
