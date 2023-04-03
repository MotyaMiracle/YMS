using AutoMapper;
using Domain.Entity;
using Domain.Services.Timeslots;
using Domain.Services.Trucks;

namespace Application.AutoMapper
{
    public class MapTimeslot : Profile
    {
        public MapTimeslot()
        {
            CreateMap<Timeslot, TimeslotDto>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(src => src.Id.ToString()));

            CreateMap<TimeslotDto, Timeslot>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(src.Id) ? Guid.NewGuid() : Guid.Parse(src.Id)));
        }
    }
}
