using Database;
using Domain.Entity;
using Domain.Enums;
using Domain.Services.Reports;
using Microsoft.EntityFrameworkCore;

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
                    var tripByDetalizationType = tripsByTc.GroupBy(t => new { t.Timeslot.Status, t.Number});
                    if (requestReport.DetailByTrip)
                    {
                        entry.SubRows = tripByDetalizationType.Select(x => new DetalizationReportRow
                        {
                            DetailType = x.Key.Status.ToString(),
                            TripsCount = x.Count(),
                            SubRows = tripByDetalizationType.Select(g => new DetalizationReportRow
                            {
                                DetailType = g.Key.Number,
                                TripsCount = g.Count()
                            }).ToList()
                        }).ToList();
                    }
                    else
                    {
                        entry.SubRows = tripByDetalizationType.Select(x => new DetalizationReportRow
                        {
                            DetailType = x.Key.Status.ToString(),
                            TripsCount = x.Count(),
                        }).ToList();
                    }
                    
                }
            }
            else
            {
                var tripsByDetalizationType = (await GetBaseQuery(requestReport)
                    .ToListAsync(token))
                    .GroupBy(t => new { t.Timeslot.Status, t.Number });
                if (requestReport.DetailByTrip)
                {
                    response.Entries = tripsByDetalizationType.Select(x => new DetalizationReportRow
                    {
                        DetailType = x.Key.Status.ToString(),
                        TripsCount = x.Count(),
                        SubRows = tripsByDetalizationType.Select(g => new DetalizationReportRow
                        {
                            DetailType = g.Key.Number,
                            TripsCount = g.Count()
                        }).ToList()
                    }).ToList();
                }
                else
                {
                    response.Entries = tripsByDetalizationType.Select(x => new DetalizationReportRow
                    {
                        DetailType = x.Key.Status.ToString(),
                        TripsCount = x.Count()
                    }).ToList();
                }
                
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
                    var tripByDetalizationType = tripsByTc.GroupBy(t => new { t.Timeslot.Minutes, t.Number });
                    if (requestReport.DetailByTrip)
                    {
                        entry.SubRows = tripByDetalizationType.Select(x => new DetalizationReportRow
                        {
                            DetailType = x.Key.Minutes.ToString(),
                            TripsCount = x.Count(),
                            SubRows = tripByDetalizationType.Select(g => new DetalizationReportRow
                            {
                                DetailType = g.Key.Number,
                                TripsCount = g.Count()
                            }).ToList()
                        }).ToList();
                    }
                    else
                    {
                        entry.SubRows = tripByDetalizationType.Select(x => new DetalizationReportRow
                        {
                            DetailType = x.Key.Minutes.ToString(),
                            TripsCount = x.Count(),
                        }).ToList();
                    }

                }
            }
            else
            {
                var tripsByDetalizationType = (await GetBaseQuery(requestReport)
                    .ToListAsync(token))
                    .GroupBy(t => new { t.Timeslot.Minutes, t.Number });
                if (requestReport.DetailByTrip)
                {
                    response.Entries = tripsByDetalizationType.Select(x => new DetalizationReportRow
                    {
                        DetailType = x.Key.Minutes.ToString(),
                        TripsCount = x.Count(),
                        SubRows = tripsByDetalizationType.Select(g => new DetalizationReportRow
                        {
                            DetailType = g.Key.Number,
                            TripsCount = g.Count()
                        }).ToList()
                    }).ToList();
                }
                else
                {
                    response.Entries = tripsByDetalizationType.Select(x => new DetalizationReportRow
                    {
                        DetailType = x.Key.Minutes.ToString(),
                        TripsCount = x.Count()
                    }).ToList();
                }

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
                    var tripByDetalizationType = tripsByTc.GroupBy(t => new { t.PalletsCount, t.Number });
                    if (requestReport.DetailByTrip)
                    {
                        entry.SubRows = tripByDetalizationType.Select(x => new DetalizationReportRow
                        {
                            DetailType = x.Key.PalletsCount.ToString(),
                            TripsCount = x.Count(),
                            SubRows = tripByDetalizationType.Select(g => new DetalizationReportRow
                            {
                                DetailType = g.Key.Number,
                                TripsCount = g.Count()
                            }).ToList()
                        }).ToList();
                    }
                    else
                    {
                        entry.SubRows = tripByDetalizationType.Select(x => new DetalizationReportRow
                        {
                            DetailType = x.Key.PalletsCount.ToString(),
                            TripsCount = x.Count(),
                        }).ToList();
                    }

                }
            }
            else
            {
                var tripsByDetalizationType = (await GetBaseQuery(requestReport)
                    .ToListAsync(token))
                    .GroupBy(t => new { t.PalletsCount, t.Number });
                if (requestReport.DetailByTrip)
                {
                    response.Entries = tripsByDetalizationType.Select(x => new DetalizationReportRow
                    {
                        DetailType = x.Key.PalletsCount.ToString(),
                        TripsCount = x.Count(),
                        SubRows = tripsByDetalizationType.Select(g => new DetalizationReportRow
                        {
                            DetailType = g.Key.Number,
                            TripsCount = g.Count()
                        }).ToList()
                    }).ToList();
                }
                else
                {
                    response.Entries = tripsByDetalizationType.Select(x => new DetalizationReportRow
                    {
                        DetailType = x.Key.PalletsCount.ToString(),
                        TripsCount = x.Count()
                    }).ToList();
                }

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
                    var tripByDetalizationType = tripsByTc.GroupBy(t => new { t.Storage.Name, t.Number });
                    if (requestReport.DetailByTrip)
                    {
                        entry.SubRows = tripByDetalizationType.Select(x => new DetalizationReportRow
                        {
                            DetailType = x.Key.Name,
                            TripsCount = x.Count(),
                            SubRows = tripByDetalizationType.Select(g => new DetalizationReportRow
                            {
                                DetailType = g.Key.Number,
                                TripsCount = g.Count()
                            }).ToList()
                        }).ToList();
                    }
                    else
                    {
                        entry.SubRows = tripByDetalizationType.Select(x => new DetalizationReportRow
                        {
                            DetailType = x.Key.Name,
                            TripsCount = x.Count(),
                        }).ToList();
                    }

                }
            }
            else
            {
                var tripsByDetalizationType = (await GetBaseQuery(requestReport)
                    .ToListAsync(token))
                    .GroupBy(t => new { t.Storage.Name, t.Number });
                if (requestReport.DetailByTrip)
                {
                    response.Entries = tripsByDetalizationType.Select(x => new DetalizationReportRow
                    {
                        DetailType = x.Key.Name,
                        TripsCount = x.Count(),
                        SubRows = tripsByDetalizationType.Select(g => new DetalizationReportRow
                        {
                            DetailType = g.Key.Number,
                            TripsCount = g.Count()
                        }).ToList()
                    }).ToList();
                }
                else
                {
                    response.Entries = tripsByDetalizationType.Select(x => new DetalizationReportRow
                    {
                        DetailType = x.Key.Name,
                        TripsCount = x.Count()
                    }).ToList();
                }

            }

            return response;

        }
    };
}

