using Database;
using Domain.Entity;
using Domain.Enums;
using Domain.Services.Reports;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Reports
{
    public class ReportService : IReportService
    {
        private readonly ApplicationContext _database;

        public ReportService(ApplicationContext database)
        {
            _database = database;
        }

        public async Task<ResponseReportDto> GetAsync(RequestReportDto reportDto, CancellationToken token)
        {
            if (reportDto == null)
                return null;

            if (!string.IsNullOrEmpty(reportDto.OperationType.ToString()))
            {
                return await FilterByOperationAsync(reportDto, token);
            }

            if (!string.IsNullOrEmpty(reportDto.Duration.ToString()))
            {
                return await FilterByDurationAsync(reportDto, token);
            }

            if (!string.IsNullOrEmpty(reportDto.PalletsCount.ToString()))
            {
                return await FilterByPalletAsync(reportDto, token);
            }

            if (!string.IsNullOrEmpty(reportDto.StorageName))
            {
                return await FilterByStorageAsync(reportDto, token);
            }

            return null;
        }


        private async Task<ResponseReportDto> FilterByOperationAsync(RequestReportDto requestReport, CancellationToken token)
        {
            ResponseReportDto response = new ResponseReportDto();

            switch (requestReport.OperationType)
            {
                case OperationType.Loading:
                    var trips = await _database.Trips
                        .Where(t => requestReport.StartDate.Date <= t.ArrivalTime.Date && t.ArrivalTime <= requestReport.EndDate.Date)
                        .Include(t => t.Timeslot)
                        .Include(c => c.Company)
                        .Where(t => t.Timeslot.Status.HasFlag(OperationType.Loading))
                        .ToListAsync();

                    response.Entries = trips.Select(x => new ReportEntryDto
                    {
                        CompanyName = x.Company.Name,
                        DetailType = "Загрузка"
                    }).ToList();
                    response.TripsCount = trips.Count;
                    break;

                case OperationType.Unloading:
                    trips = await _database.Trips
                       .Where(t => requestReport.StartDate.Date <= t.ArrivalTime.Date && t.ArrivalTime <= requestReport.EndDate.Date)
                       .Include(t => t.Timeslot)
                       .Include(c => c.Company)
                       .Where(t => t.Timeslot.Status.HasFlag(OperationType.Unloading))
                       .ToListAsync();

                    response.Entries = trips.Select(x => new ReportEntryDto
                    {
                        CompanyName = x.Company.Name,
                        DetailType = "Разгрузка"
                    }).ToList();
                    response.TripsCount = trips.Count;
                    break;
            }

            return response;

        }


        private async Task<ResponseReportDto> FilterByDurationAsync(RequestReportDto requestReport, CancellationToken token)
        {
            var trips = await _database.Trips
                        .Where(t => requestReport.StartDate.Date <= t.ArrivalTime.Date && t.ArrivalTime <= requestReport.EndDate.Date)
                        .Include(c => c.Company)
                        .Include(g => g.Gate)
                        .Where(t => (Math.Ceiling(((double)t.Gate.PalletHandlingTime * t.PalletsCount) / 30) * 30) == requestReport.Duration)
                        .ToListAsync();

            return new ResponseReportDto
            {
                Entries = trips.Select(x => new ReportEntryDto
                {
                    CompanyName = x.Company.Name,
                    DetailType = $"Продолжительность в минутах: {requestReport.Duration}"
                }).ToList(),
                TripsCount = trips.Count
            };
        }


        private async Task<ResponseReportDto> FilterByPalletAsync(RequestReportDto requestReport, CancellationToken token)
        {
            var trips = await _database.Trips
                        .Where(t => requestReport.StartDate.Date <= t.ArrivalTime.Date && t.ArrivalTime <= requestReport.EndDate.Date)
                        .Include(c => c.Company)
                        .Where(t => t.PalletsCount == requestReport.PalletsCount)
                        .ToListAsync();

            return new ResponseReportDto
            {
                Entries = trips.Select(x => new ReportEntryDto
                {
                    CompanyName = x.Company.Name,
                    DetailType = $"Количество паллет: {requestReport.PalletsCount}"
                }).ToList(),
                TripsCount = trips.Count
            };
        }

        private async Task<ResponseReportDto> FilterByStorageAsync(RequestReportDto requestReport, CancellationToken token)
        {
            var trips = await _database.Trips
                        .Where(t => requestReport.StartDate.Date <= t.ArrivalTime.Date && t.ArrivalTime <= requestReport.EndDate.Date)
                        .Include(c => c.Company)
                        .Include(s => s.Storage)
                        .Where(t => t.Storage.Name == requestReport.StorageName)
                        .ToListAsync();

            return new ResponseReportDto
            {
                Entries = trips.Select(x => new ReportEntryDto
                {
                    CompanyName = x.Company.Name,
                    DetailType = $"{requestReport.StorageName}"
                }).ToList(),
                TripsCount = trips.Count
            };
        }
    }
}
