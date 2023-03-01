using AutoMapper;
using Domain.Services.Trips;
using Yard_Management_System.Entity;

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
