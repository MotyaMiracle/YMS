﻿using AutoMapper;
using Database.Entity;
using Microsoft.EntityFrameworkCore;
using Yard_Management_System;

namespace Domain.Services.Gates
{
    public class GateService : IGatesService
    {
        private readonly ApplicationContext _database;
        private readonly IMapper _mapper;

        public GateService(ApplicationContext database, IMapper mapper)
        {
            _database = database;
            _mapper = mapper;
        }

        public async Task CreateAndUpdateAsync(GateDto gateDto, CancellationToken token)
        {
            //Create
            if (string.IsNullOrWhiteSpace(gateDto.Id))
            {
                Gate gate = _mapper.Map<Gate>(gateDto);
                await _database.Gates.AddAsync(gate,token);
            }
            //Update
            else
            {
                Gate updateGates = _mapper.Map<Gate>(gateDto);
                _database.Gates.Update(updateGates);
            }
            await _database.SaveChangesAsync(token);
        }

        public async Task DeleteGateAsync(Guid gateId, CancellationToken token)
        {
            Gate gate = await _database.Gates
                .FirstOrDefaultAsync(s => s.Id == gateId, token);

            _database.Gates.Remove(gate);
            await _database.SaveChangesAsync(token);
        }

        public async Task<GateEntriesDto> GetAllAsync(CancellationToken token)
        {
            var gates = await _database.Gates.ToListAsync(token);

            return new GateEntriesDto
            {
                Entries = _mapper.Map<IEnumerable<GateDto>>(gates).ToList()
            };
        }

        public async Task<GateDto> GetAsync(Guid gateId, CancellationToken token)
        {
            var gate = await _database.Gates
                .FirstOrDefaultAsync(s => s.Id == gateId, token);

            var response = _mapper.Map<GateDto>(gate);

            return response;
        }
    }
}
