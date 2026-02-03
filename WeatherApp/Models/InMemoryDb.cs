using WeatherApp.Measurements;

namespace WeatherApp.Models
{
    public interface IInMemoryDb
    {
        /// <summary>
        /// Collection of city data.
        /// </summary>
        Dictionary<string, City> Cities { get; }
        /// <summary>
        /// Fetch data from the source and populate the in-memory database.
        /// </summary>
        Task Fetch();
    }
    public class InMemoryDb : IInMemoryDb
    {
        public Dictionary<string, City> Cities { get; private set; } = [];

        public async Task Fetch()
        {
            Cities = await MeasurementReader.ReadAsync();
        }
    }
}
