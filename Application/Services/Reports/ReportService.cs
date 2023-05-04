using Database;
using Domain.Entity;
using Domain.Enums;
using Domain.Services.Reports;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

            var trips = new Dictionary<string, List<Trip>>();

            if (reportDto.DetailByCompany)
            {
                trips = GetBaseQuery(reportDto)
                    .GroupBy(x => x.Company.Name)
                    .ToDictionary(x => x.Key, x => x.ToList());
            }

            switch (reportDto.FilterDetalization)
            {
                case FilterDetalization.OperationType:
                    return await FilterByOperationAsync(reportDto, trips, token);

                case FilterDetalization.Duration:
                    return await FilterByDurationAsync(reportDto, trips, token);

                case FilterDetalization.Pallets:
                    return await FilterByPalletAsync(reportDto, trips, token);

                case FilterDetalization.Storage:
                    return await FilterByStorageAsync(reportDto, trips, token);
            }
            return null;
        }

        private IQueryable<Trip> GetBaseQuery(RequestReportDto requestReport)
        {
            return _database.Trips
                .Where(t => requestReport.StartDate.Date <= t.ArrivalTime.Date && t.ArrivalTime <= requestReport.EndDate.Date)
                .Include(c => c.Company)
                .Include(t => t.Timeslot)
                .Include(s => s.Storage);
        }

        private async Task<ResponseReportDto> FilterByOperationAsync(RequestReportDto requestReport, Dictionary<string, List<Trip>> trips, CancellationToken token)
        {
            ResponseReportDto response = new();

            if (trips != null && trips.Any())
            {
                response.Entries = trips.Select(x => new DetalizationReportRow
                {
                    DetailType = x.Key,
                    TripsCount = x.Value.Count
                }).ToList();

                foreach (var entry in response.Entries)
                {
                    var tripsByTc = trips[entry.DetailType];
                    var tripByDetalizationType = tripsByTc.GroupBy(t => t.Timeslot.Status);
                    entry.SubRows = tripByDetalizationType.Select(x => new DetalizationReportRow
                    {
                        DetailType = x.Key.ToString(),
                        TripsCount = x.Count()
                    }).ToList();
                }
            }
            else
            {
                var tripsByDetalizationType = (await GetBaseQuery(requestReport).ToListAsync(token)).GroupBy(x => x.Timeslot.Status);
                response.Entries = tripsByDetalizationType.Select(x => new DetalizationReportRow
                {
                    DetailType = x.Key.ToString(),
                    TripsCount = x.Count()
                }).ToList();
            }

            return response;
        }


        private async Task<ResponseReportDto> FilterByDurationAsync(RequestReportDto requestReport, Dictionary<string, List<Trip>> trips, CancellationToken token)
        {
            ResponseReportDto response = new();

            if (trips != null && trips.Any())
            {
                response.Entries = trips.Select(x => new DetalizationReportRow
                {
                    DetailType = x.Key,
                    TripsCount = x.Value.Count
                }).ToList();

                foreach (var entry in response.Entries)
                {
                    var tripsByTc = trips[entry.DetailType];
                    var tripByDetalizationType = tripsByTc.GroupBy(t => t.Timeslot.Minutes);
                    entry.SubRows = tripByDetalizationType.Select(x => new DetalizationReportRow
                    {
                        DetailType = x.Key.ToString(),
                        TripsCount = x.Count()
                    }).ToList();
                }
            }
            else
            {
                var tripsByDetalizationType = (await GetBaseQuery(requestReport).ToListAsync(token)).GroupBy(x => x.Timeslot.Minutes);
                response.Entries = tripsByDetalizationType.Select(x => new DetalizationReportRow
                {
                    DetailType = x.Key.ToString(),
                    TripsCount = x.Count()
                }).ToList();
            }

            return response;

        }

        private async Task<ResponseReportDto> FilterByPalletAsync(RequestReportDto requestReport, Dictionary<string, List<Trip>> trips, CancellationToken token)
        {
            ResponseReportDto response = new();

            if (trips != null && trips.Any())
            {
                response.Entries = trips.Select(x => new DetalizationReportRow
                {
                    DetailType = x.Key,
                    TripsCount = x.Value.Count
                }).ToList();

                foreach (var entry in response.Entries)
                {
                    var tripsByTc = trips[entry.DetailType];
                    var tripByDetalizationType = tripsByTc.GroupBy(t => t.PalletsCount);
                    entry.SubRows = tripByDetalizationType.Select(x => new DetalizationReportRow
                    {
                        DetailType = x.Key.ToString(),
                        TripsCount = x.Count()
                    }).ToList();
                }
            }
            else
            {
                var tripsByDetalizationType = (await GetBaseQuery(requestReport).ToListAsync(token)).GroupBy(x => x.PalletsCount);
                response.Entries = tripsByDetalizationType.Select(x => new DetalizationReportRow
                {
                    DetailType = x.Key.ToString(),
                    TripsCount = x.Count()
                }).ToList();
            }

            return response;
        }

        private async Task<ResponseReportDto> FilterByStorageAsync(RequestReportDto requestReport, Dictionary<string, List<Trip>> trips, CancellationToken token)
        {
            ResponseReportDto response = new();

            if (trips != null && trips.Any())
            {
                response.Entries = trips.Select(x => new DetalizationReportRow
                {
                    DetailType = x.Key,
                    TripsCount = x.Value.Count
                }).ToList();

                foreach (var entry in response.Entries)
                {
                    var tripsByTc = trips[entry.DetailType];
                    var tripByDetalizationType = tripsByTc.GroupBy(t => t.Storage.Name);
                    entry.SubRows = tripByDetalizationType.Select(x => new DetalizationReportRow
                    {
                        DetailType = x.Key.ToString(),
                        TripsCount = x.Count()
                    }).ToList();
                }
            }
            else
            {
                var tripsByDetalizationType = (await GetBaseQuery(requestReport).ToListAsync(token)).GroupBy(x => x.Storage.Name);
                response.Entries = tripsByDetalizationType.Select(x => new DetalizationReportRow
                {
                    DetailType = x.Key.ToString(),
                    TripsCount = x.Count()
                }).ToList();
            }

            return response;
        }
    };
}

