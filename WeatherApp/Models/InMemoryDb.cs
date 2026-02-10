using WeatherApp.Measurements;
using WeatherApp.Models;

public interface IInMemoryDb
{
    /// <summary>
    /// Collection of city data.
    /// </summary>
    Dictionary<string, CityData> CitiesData { get; }
    /// <summary>
    /// Fetch data from the source and populate the in-memory database.
    /// </summary>
    Task Fetch();
}

public class InMemoryDb : IInMemoryDb
{
    public Dictionary<string, CityData> CitiesData { get; private set; } = [];

    public async Task Fetch()
    {
        var newCityData = await MeasurementReader.ReadAsync();
        if (newCityData is not null)
            CitiesData = newCityData;
    }
}
