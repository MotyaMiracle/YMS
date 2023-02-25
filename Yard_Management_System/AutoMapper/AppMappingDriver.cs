using AutoMapper;
using Yard_Management_System.Entity;
using Yard_Management_System.Services.Drivers;

namespace Yard_Management_System.AutoMapper
{
    public class AppMappingDriver : Profile
    {
        public AppMappingDriver() 
        {
            CreateMap<Driver, DriverDto>().ReverseMap();
        }
    }
}
