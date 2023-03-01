using AutoMapper;
using Domain.Services.Users;
using Yard_Management_System.Entity;

namespace Yard_Management_System.AutoMapper
{
    public class MapUser : Profile
    {
        public MapUser() 
        {
            CreateMap<User, UserDto>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(src => src.Id.ToString()));
            CreateMap<UserDto, User>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(src.Id) ? Guid.NewGuid() : Guid.Parse(src.Id)));
        }
    }
}
