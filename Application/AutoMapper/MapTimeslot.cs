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
                .ForPath(dest => dest.TripId.Value, opt => opt.MapFrom(src => src.TripId.ToString()))
                .ForMember(dto => dto.Id, opt => opt.MapFrom(src => src.Id.ToString()));

            CreateMap<TimeslotDto, Timeslot>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(src.Id) ? Guid.NewGuid() : Guid.Parse(src.Id)))
                .ForMember(dest => dest.TripId, opt => opt.MapFrom(src => Guid.Parse(src.TripId.Value)));
        }
    }
}
