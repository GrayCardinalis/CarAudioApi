using Scalar.AspNetCore;
using Microsoft.EntityFrameworkCore;
using CarAudioApi.Data;
using CarAudioApi.Services;
using CarAudioApi.DTOs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")).UseSnakeCaseNamingConvention());

builder.Services.AddScoped<IAudioComponentService, AudioComponentService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(); // Рисуем UI Scalar по адресу /scalar
}

app.MapControllers();

app.Run();
