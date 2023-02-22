using AutoMapper;
using Domain.Services.User;
using Yard_Management_System.Entity;

namespace Yard_Management_System.AutoMapper
{
    public class MapUser : Profile
    {
        public MapUser() 
        {
            CreateMap<UserDto, UserEntity>();
        }
    }
}
