using AutoMapper;
using Domain.Services.Trips;
using Yard_Management_System.Entity;

namespace Yard_Management_System.AutoMapper
{
    public class MapTrip : Profile
    {
        public MapTrip()
        {
            CreateMap<TripDto, Trip>();
        }
    }
}
