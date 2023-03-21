using Application.AutoMapper;
using Application.Services.Companies;
using Application.Services.Drivers;
using Application.Services.Files;
using Application.Services.Gates;
using Application.Services.History;
using Application.Services.Storages;
using Application.Services.Trailers;
using Application.Services.Trips;
using Application.Services.Trucks;
using Application.Services.Users;
using Domain.Services.Companies;
using Domain.Services.Drivers;
using Domain.Services.Files;
using Domain.Services.Gates;
using Domain.Services.History;
using Domain.Services.Storages;
using Domain.Services.Trailers;
using Domain.Services.Trips;
using Domain.Services.Trucks;
using Domain.Services.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Install
{
    public static class DomainInstaller
    {
        public static void AddDomain(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IHistoryService, HistoryService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITripService, TripService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IUserProvider, UserProvider>();
            services.AddScoped<IDriverService, DriverService>();
            services.AddScoped<IStorageService, StorageService>();
            services.AddScoped<ITruckService, TruckService>();
            services.AddScoped<IGatesService, GateService>();
            services.AddScoped<ITrailerService, TrailerService>();
            services.AddScoped<ICompanyService, CompanyService>();

            services.AddAutoMapper(
                typeof(MapUser),
                typeof(MapTrip),
                typeof(MapFile),
                typeof(AppMappingDriver),
                typeof(AppMappingStorage),
                typeof(MapTruck),
                typeof(AppMappingGate),
                typeof(MapTrailer),
                typeof(AppMappingCompany)
    );

        }
    }
}
