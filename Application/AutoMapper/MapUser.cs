using Application.Services.Identity;
using AutoMapper;
using Domain.Entity;
using Domain.Services.Users;

namespace Application.AutoMapper
{
    public class MapUser : Profile
    {
        public MapUser() 
        {
            CreateMap<User, UserDto>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForPath(dest => dest.RoleId.Value, opt => opt.MapFrom(src => src.RoleId.ToString()))
                .ForPath(dest => dest.RoleId.Name, opt => opt.MapFrom(src => src.Role.Name));
            CreateMap<UserDto, User>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(src.Id) ? Guid.NewGuid() : Guid.Parse(src.Id)))
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => Authorization.GetHash(src.Password)))
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => Guid.Parse(src.RoleId.Value)));

        }
    }
}
