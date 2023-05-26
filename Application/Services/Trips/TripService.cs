using AutoMapper;
using Database;
using Domain.Entity;
using Domain.Enums;
using Domain.Services.Color;
using Domain.Services.History;
using Domain.Services.Storages;
using Domain.Services.Trips;
using Domain.Services.Users;
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
        private readonly ApplicationContext _database;
        private readonly IBackligth _backlightService;

        public TripService(ApplicationContext db, IMapper mapper, IHistoryService historyService, IUserProvider userProvider, IBackligth backlightService)
        {
            _mapper = mapper;
            _database = db;
            _historyService = historyService;
            _userProvider = userProvider;
            _backlightService = backlightService;
        }


        public async Task CreateAsync(TripDto trip, CancellationToken token)
        {
            trip.NowStatus = TripStatus.Create;
            Trip newTrip = _mapper.Map<Trip>(trip);
            newTrip.QRCode = GenerateQRCode(newTrip.Number);
            await _historyService.SaveAsync(newTrip.Id, "Создана путёвка", await _userProvider.GetCurrentUserAsync(token), token);
            await _database.Trips.AddAsync(newTrip, token);
            await _database.SaveChangesAsync(token);
        }

        public async Task<TripDto> GetAsync(Guid tripId, CancellationToken token)
        {
            var trip = await _database.Trips
                            .FirstOrDefaultAsync(s => s.Id == tripId, token);

            if (trip is null)
                return null;

            var response = _mapper.Map<TripDto>(trip);

            return response;
        }

        public async Task OccupancyAsync(Trip trip, CancellationToken token)
        {
            if (trip is null)
                return;

            var storage = await _database.Storages.FirstOrDefaultAsync(s => s.Id == trip.StorageId, token);

            var timeslot = await _database.Timeslots.FirstOrDefaultAsync(t => t.Id == trip.TimeslotId, token);

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

            await _database.SaveChangesAsync(token);
        }
        
        public async Task<BackligthDto> BackligthAsync(string entityId, CancellationToken token)
        {
            Trip trip = await _database.Trips
                .Include(t => t.Truck)
                .Include(t => t.Timeslot)
                .FirstOrDefaultAsync(t => t.Id == Guid.Parse(entityId));

            BackligthDto backligth = new BackligthDto();

            var temp = await _backlightService.IsActive(entityId, token);

            backligth.Backlight = _backlightService.Type;

            return backligth;
        }

        public void Create(Guid entityId)
        {
            throw new NotImplementedException();
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
