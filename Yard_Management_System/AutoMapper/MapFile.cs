using AutoMapper;
using Domain.Services.Files;
using Yard_Management_System.Entity;

namespace Yard_Management_System.AutoMapper
{
    public class MapFile : Profile
    {
        public MapFile()
        {
            CreateMap<FileDto, EntityFile>().ReverseMap();
        }
    }
}
