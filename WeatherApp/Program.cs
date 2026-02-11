using WeatherApp.Adapters;
using WeatherApp.HoistedServices;
using WeatherApp.Repozitories;
using WeatherApp.Services;
using WeatherApp.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// In-Memory Database
builder.Services.AddSingleton<IInMemoryDb, InMemoryDb>();
builder.Services.AddHostedService<InMemoryDbInitHostedService>();

// Adapters
builder.Services.AddScoped<ICityDataAdapter, CityDataAdapter>();

// Repozitories
builder.Services.AddScoped<ICityDataRepozitory, CityDataInMemoryRepozitory>();

// Services
builder.Services.AddScoped<IDataBaseService, DataBaseInMemoryService>();

// Validators
builder.Services.AddScoped<ICityDataValidator, CityDataValidator>();

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
