using AutoMapper;
using Yard_Management_System.CRUDs.Storage;
using Yard_Management_System.Entity;

namespace Yard_Management_System.AutoMapper
{
    public class AppMappingStorage : Profile
    {
        public AppMappingStorage()
        {
            CreateMap<CreateStorage, Storage>();
        }
    }
}
