using Domain.Services.History;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text.Json.Serialization;
using Yard_Management_System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.
    AddDbContext<ApplicationContext>(options => 
    options.UseNpgsql(connection, b => b.MigrationsAssembly("Database")));

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});

builder.Services.AddScoped<IHistoryService, HistoryService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = SigningOptions.SignIssuer,
            ValidAudience = SigningOptions.SignAudience,
            IssuerSigningKey = SigningOptions.GetSymmetricSecurityKey(),
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddControllers();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("OnlyForAdmin", policy =>
    {
        policy.RequireClaim(ClaimsIdentity.DefaultRoleClaimType, "Гл. Администратор");
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.MapControllers();

app.MapGet("/", () => Results.Redirect("api/login"));


app.MapGet("api/login", async (HttpContext context) =>
{
    context.Response.ContentType = "text/html; charset=utf-8";
    // html-ôîðìà äëÿ ââîäà ëîãèíà/ïàðîëÿ
    string loginForm = @"<!DOCTYPE html>
    <html>
    <head>
        <meta charset='utf-8' />
        <title>METANIT.COM</title>
    </head>
    <body>
        <h2>Login Form</h2>
        <form method='post'>
            <p>
                <label>Login</label><br />
                <input name='login' />
            </p>
            <p>
                <label>Password</label><br />
                <input type='password' name='password' />
            </p>
            <input type='submit' value='Login' />
        </form>
    </body>
    </html>";
    await context.Response.WriteAsync(loginForm);
});

app.Run();
