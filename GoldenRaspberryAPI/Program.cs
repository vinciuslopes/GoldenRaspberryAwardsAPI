using GoldenRaspberryAPI.Data;
using GoldenRaspberryAPI.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("MoviesDB"));

builder.Services.AddTransient<CsvReaderService>();
builder.Services.AddTransient<AwardIntervalService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Popular banco com dados do CSV
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var csvReaderService = scope.ServiceProvider.GetRequiredService<CsvReaderService>();

    var movies = csvReaderService.ReadCsv("Resources/Movielist.csv");
    context.Movies.AddRange(movies);
    context.SaveChanges();
}

app.Run();

// Faz com que a classe Program seja pública e acessível ao projeto de teste
public partial class Program { }
