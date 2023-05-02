﻿using AutoMapper;
using Database;
using Domain.Entity;
using Domain.Enums;
using Domain.Services.History;
using Domain.Services.Trips;
using Domain.Services.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using QRCoder;
using System.Drawing;

namespace Application.Services.Trips
{
    public class TripService : ITripService
    {
        private readonly IMapper _mapper;
        private readonly IHistoryService _historyService;
        private readonly IUserProvider _userProvider;
        private readonly ApplicationContext _db;

        public TripService(ApplicationContext db, IMapper mapper, IHistoryService historyService, IUserProvider userProvider)
        {
            _mapper = mapper;
            _db = db;
            _historyService = historyService;
            _userProvider = userProvider;
        }
        public void Create(Guid entityId)
        {
            throw new NotImplementedException();
        }

        public async Task CreateAsync(TripDto trip, CancellationToken token)
        {
            trip.NowStatus = TripStatus.Create;
            Trip newTrip = _mapper.Map<Trip>(trip);
            newTrip.QRCode = GenerateQRCode(newTrip.Number);
            await _historyService.SaveAsync(newTrip.Id, "Создана путёвка", await _userProvider.GetCurrentUserAsync(token), token);
            await _db.Trips.AddAsync(newTrip, token);
            await _db.SaveChangesAsync(token);
        }

        public async Task OccupancyAsync(Trip trip, CancellationToken token)
        {
            if (trip is null)
                return;

            var storage = await _db.Storages.FirstOrDefaultAsync(s => s.Id == trip.StorageId, token);

            var timeslot = await _db.Timeslots.FirstOrDefaultAsync(t => t.Id == trip.TimeslotId, token);

            if (storage is null && timeslot is null)
                return;


            switch (timeslot.Status)
            {
                case OperationType.Loading:
                        storage.OccupancyActual -= trip.PalletsCount;
                    break;

                case OperationType.Unloading:
                        storage.OccupancyActual += trip.PalletsCount;
                    break;
            }

            await _db.SaveChangesAsync(token);
        }

        private byte[] GenerateQRCode(string number)
        {
            QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
            QRCodeData qRCodeData = qRCodeGenerator.CreateQrCode(number, QRCodeGenerator.ECCLevel.Q);
            QRCode qRCode = new QRCode(qRCodeData);
            Bitmap bitmap = qRCode.GetGraphic(15);
            var bitmapBytes = ConvertBitmapToBytes(bitmap);
            return bitmapBytes;
        }

        private byte[] ConvertBitmapToBytes(Bitmap bitmap)
        {
            using (var stream = new MemoryStream())
            {
                bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }
    }
}
