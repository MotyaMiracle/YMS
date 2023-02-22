using AutoMapper;
using Yard_Management_System.CRUDs.Driver;
using Yard_Management_System.Entity;

namespace Yard_Management_System.AutoMapper
{
    public class AppMappingDriver : Profile
    {
        public AppMappingDriver() 
        {
            CreateMap<CreateDriver, Driver>();
        }
    }
}
