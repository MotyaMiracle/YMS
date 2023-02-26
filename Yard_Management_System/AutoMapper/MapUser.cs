using AutoMapper;
using Domain.Services.Users;
using Yard_Management_System.Entity;

namespace Yard_Management_System.AutoMapper
{
    public class MapUser : Profile
    {
        public MapUser() 
        {
            CreateMap<UserDto, User>().ReverseMap();
        }
    }
}
