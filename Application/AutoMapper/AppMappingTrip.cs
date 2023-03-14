using AutoMapper;
using Domain.Entity;
using Domain.Services.Trips;

namespace Yard_Management_System.AutoMapper
{
    public class AppMappingTrip : Profile
    {
        public AppMappingTrip() 
        {
            CreateMap<TripDto, Trip>();
        }
    }
}
