using WeatherApp.Models;
using WeatherApp.Repozitories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// In-Memory Database
builder.Services.AddSingleton<IInMemoryDb, InMemoryDb>();
builder.Services.AddHostedService<InMemoryDbInitHostedService>();

// Repozitories
builder.Services.AddScoped<ICityDataRepozitory, CityDataRepozitory>();

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

app.Run();
