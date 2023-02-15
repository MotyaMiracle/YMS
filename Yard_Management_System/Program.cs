using Yard_Management_System;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using System.IdentityModel.Tokens.Jwt;
using Serilog;
using Microsoft.Extensions.Options;
using Yard_Management_System.Entity;

var builder = WebApplication.CreateBuilder(args);

string connection = "Host=localhost;Port=5432;Database=yms_db;Username=postgres;Password=13245";
builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connection));

builder.Services.AddControllers();

var app = builder.Build();
app.MapGet("/", () => "Hello World!");
app.MapControllers();
app.Run();
