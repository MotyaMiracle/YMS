﻿using AutoMapper;
using Domain.Entity;
using Domain.Services.Storages;

namespace Application.AutoMapper
{
    public class AppMappingStorage : Profile
    {
        public AppMappingStorage()
        {
            CreateMap<Storage, StorageDto>()
               .ForMember(dto => dto.Id, opt => opt.MapFrom(src => src.Id.ToString()));
            CreateMap<StorageDto, Storage>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(src.Id) ? Guid.NewGuid() : Guid.Parse(src.Id)));
        }
    }
}
