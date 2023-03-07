﻿using AutoMapper;
using Database.Entity;
using Domain.Services.Drivers;
using Microsoft.EntityFrameworkCore;
using Yard_Management_System;
using Yard_Management_System.Entity;

namespace Domain.Services.Trucks
{
    public class TruckService : ITruckService
    {
        private readonly ApplicationContext _db;
        private readonly IMapper _mapper;

        public TruckService(ApplicationContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task CreateAndUpdateAsync(TruckDto truckDto, CancellationToken token)
        {
            //Create
            if (string.IsNullOrWhiteSpace(truckDto.Id))
            {
                Truck truck = _mapper.Map<Truck>(truckDto);
                //truck.CompanyId = Guid.NewGuid();
                await _db.Trucks.AddAsync(truck, token);
            }
            //Update
            else
            {
                Truck updateTruck = _mapper.Map<Truck>(truckDto);
                _db.Trucks.Update(updateTruck);
            }
            await _db.SaveChangesAsync(token);
        }

        public async Task DeleteAsync(Guid truckId, CancellationToken token)
        {
            Truck truck = await _db.Trucks
                .FirstOrDefaultAsync(d => d.Id == truckId, token);

            _db.Trucks.Remove(truck);
            await _db.SaveChangesAsync(token);
        }

        public async Task<TruckEntriesDto> GetAllAsync(CancellationToken token)
        {
            var trucks = await _db.Trucks.ToListAsync(token);

            return new TruckEntriesDto
            {
                Entries = _mapper.Map<IEnumerable<TruckDto>>(trucks).ToList()
            };
        }

        public async Task<TruckDto> GetAsync(Guid truckId, CancellationToken token)
        {
            var truck = await _db.Trucks
                .FirstOrDefaultAsync(s => s.Id == truckId, token);

            var response = _mapper.Map<TruckDto>(truck);

            return response;
        }
    }
}