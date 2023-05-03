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
                    
                //case FilterDetalization.Duration:
                //    return await FilterByDurationAsync(reportDto, token);
                    
                //case FilterDetalization.Pallets:
                //    return await FilterByPalletAsync(reportDto, token);
                    
                //case FilterDetalization.Storage:
                //    return await FilterByStorageAsync(reportDto, token);  
            }
            return null;
        }

        private IQueryable<Trip> GetBaseQuery(RequestReportDto requestReport)
        {
            return _database.Trips
                .Where(t => requestReport.StartDate.Date <= t.ArrivalTime.Date && t.ArrivalTime <= requestReport.EndDate.Date)
                .Include(c => c.Company)
                .Include(t => t.Timeslot);
        }

        private async Task<ResponseReportDto> FilterByOperationAsync(RequestReportDto requestReport, Dictionary<string, List<Trip>> trips, CancellationToken token)
        {
            ResponseReportDto response = new ResponseReportDto();
            response.Entries = new List<DetalizationReportRow> 
            {
                
            }
            var tripByDetalizationType = trips.Values.SelectMany(x => x).GroupBy(t => t.Timeslot.Status);



            response.Entries = tripByDetalizationType.Select(x => new DetalizationReportRow
            {
                DetailType = 
                TripsCount = trips.Count(),
                SubRows = new List<DetalizationReportRow>
                        {
                            new DetalizationReportRow
                            {
                                DetailType = "Загрузка",
                                TripsCount = x.Count
                            }
                        }
            }).ToList();
            
            return response;
        } 


        //private async Task<ResponseReportDto> FilterByDurationAsync(RequestReportDto requestReport, CancellationToken token)
        //{
        //    var trips = await _database.Trips
        //                .Where(t => requestReport.StartDate.Date <= t.ArrivalTime.Date && t.ArrivalTime <= requestReport.EndDate.Date)
        //                .Include(c => c.Company)
        //                .Where(t => (Math.Ceiling(((double)t.Gate.PalletHandlingTime * t.PalletsCount) / 30) * 30) == requestReport.Duration)
        //                .GroupBy(g => g.Company.Name)
        //                .Select(g => new
        //                {
        //                    CompanyName = g.Key,
        //                    Count = g.Count(),
        //                })
        //                .ToListAsync();
        //    if (requestReport.DetailByCompany == true)
        //    {
        //        return new ResponseReportDto
        //        {
        //            Entries = trips.Select(x => new DetalizationReportRow
        //            {
        //                DetailType = x.CompanyName,
        //                TripsCount = trips.Count(),
        //                SubRows = new List<DetalizationReportRow>
        //            {
        //                new DetalizationReportRow
        //                {
        //                    DetailType = $"Продолжительность в минутах: {requestReport.Duration}",
        //                    TripsCount = x.Count
        //                }
        //            }
        //            }).ToList()
        //        };
        //    }
        //    else
        //    {
        //        return new ResponseReportDto
        //        {
        //            Entries = trips.Select(x => new DetalizationReportRow
        //            {
        //                DetailType = $"Продолжительность в минутах: {requestReport.Duration}",
        //                TripsCount = trips.Count(),
        //                SubRows = new List<DetalizationReportRow>
        //            {
        //                new DetalizationReportRow
        //                {
        //                    TripsCount = x.Count
        //                }
        //            }
        //            }).ToList()
        //        };
        //    }

        //}   

        //private async Task<ResponseReportDto> FilterByPalletAsync(RequestReportDto requestReport, CancellationToken token)
        //{
        //    var trips = await _database.Trips
        //                .Where(t => requestReport.StartDate.Date <= t.ArrivalTime.Date && t.ArrivalTime <= requestReport.EndDate.Date)
        //                .Include(c => c.Company)
        //                .Where(t => t.PalletsCount == requestReport.PalletsCount)
        //                .GroupBy(g => g.Company.Name)
        //                .Select(g => new
        //                {
        //                    CompanyName = g.Key,
        //                    Count = g.Count(),
        //                })
        //                .ToListAsync();

        //    if (requestReport.DetailByCompany == true)
        //    {
        //        return new ResponseReportDto
        //        {
        //            Entries = trips.Select(x => new DetalizationReportRow
        //            {
        //                DetailType = x.CompanyName,
        //                TripsCount = trips.Count(),
        //                SubRows = new List<DetalizationReportRow>
        //            {
        //                new DetalizationReportRow
        //                {
        //                    DetailType = $"Количество паллет: {requestReport.PalletsCount}",
        //                    TripsCount = x.Count
        //                }
        //            }
        //            }).ToList()
        //        };
        //    }
        //    else
        //    {
        //        return new ResponseReportDto
        //        {
        //            Entries = trips.Select(x => new DetalizationReportRow
        //            {
        //                DetailType = $"Количество паллет: {requestReport.PalletsCount}",
        //                TripsCount = trips.Count(),
        //                SubRows = new List<DetalizationReportRow>
        //            {
        //                new DetalizationReportRow
        //                {
        //                    TripsCount = x.Count
        //                }
        //            }
        //            }).ToList()
        //        };
        //    }
        //}

        //private async Task<ResponseReportDto> FilterByStorageAsync(RequestReportDto requestReport, CancellationToken token)
        //{
        //    var trips = await _database.Trips
        //                .Where(t => requestReport.StartDate.Date <= t.ArrivalTime.Date && t.ArrivalTime <= requestReport.EndDate.Date)
        //                .Include(c => c.Company)
        //                .Include(s => s.Storage)
        //                .Where(t => t.Storage.Name == requestReport.StorageName)
        //                .GroupBy(g => g.Company.Name)
        //                .Select(g => new
        //                {
        //                    CompanyName = g.Key,
        //                    Count = g.Count(),
        //                })
        //                .ToListAsync();

        //    if (requestReport.DetailByCompany == true)
        //    {
        //        return new ResponseReportDto
        //        {
        //            Entries = trips.Select(x => new DetalizationReportRow
        //            {
        //                DetailType = x.CompanyName,
        //                TripsCount = trips.Count(),
        //                SubRows = new List<DetalizationReportRow>
        //            {
        //                new DetalizationReportRow
        //                {
        //                    DetailType = $"Склад: {requestReport.StorageName}",
        //                    TripsCount = x.Count
        //                }
        //            }
        //            }).ToList()
        //        };
        //    }
        //    else
        //    {
        //        return new ResponseReportDto
        //        {
        //            Entries = trips.Select(x => new DetalizationReportRow
        //            {
        //                DetailType = $"Склад: {requestReport.StorageName}",
        //                TripsCount = trips.Count(),
        //                SubRows = new List<DetalizationReportRow>
        //            {
        //                new DetalizationReportRow
        //                {
        //                    TripsCount = x.Count
        //                }
        //            }
        //            }).ToList()
        //        };
        //    }
        //}
    };
}

