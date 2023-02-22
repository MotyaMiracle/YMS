using AutoMapper;
using Domain.Services.Trip;
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
