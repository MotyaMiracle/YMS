using Yard_Management_System;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using System.IdentityModel.Tokens.Jwt;
using Serilog;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

string connection = "Host=localhost;Port=5432;Database=yms_db;Username=postgres;Password=13245";
builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connection));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/roles", (ApplicationContext db) => db.Roles.ToList());

app.Run();
