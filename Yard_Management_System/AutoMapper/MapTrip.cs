using AutoMapper;
using Domain.Services.Trips;
using Yard_Management_System.Entity;

namespace Yard_Management_System.AutoMapper
{
    public class MapTrip : Profile
    {
        public MapTrip()
        {
            CreateMap<Trip, TripDto>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForPath(dest => dest.StorageId.Value, opt => opt.MapFrom(src => src.StorageId.ToString()))
                .ForPath(dest => dest.DriverId.Value, opt => opt.MapFrom(src => src.DriverId.ToString()))
                .ForPath(dest => dest.TruckId.Value, opt => opt.MapFrom(src => src.TruckId.ToString()));

            CreateMap<TripDto, Trip>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(src.Id) ? Guid.NewGuid() : Guid.Parse(src.Id)))
                .ForMember(dest => dest.StorageId, opt => opt.MapFrom(src => Guid.Parse(src.StorageId.Value)))
                .ForMember(dest => dest.DriverId, opt => opt.MapFrom(src => Guid.Parse(src.DriverId.Value)))
                .ForMember(dest => dest.TruckId, opt => opt.MapFrom(src => Guid.Parse(src.TruckId.Value)));

        }
    }
}
