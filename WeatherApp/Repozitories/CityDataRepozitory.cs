using WeatherApp.Models;

namespace WeatherApp.Repozitories
{
    public interface ICityDataRepozitory
    {
        /// <summary>
        /// Asynchronously retrieves all available city data records.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of <see
        /// cref="CityData"/> objects representing all cities. The collection is empty if no city data is available.</returns>
        Task<IEnumerable<CityData>> GetAll();
        /// <summary>
        /// Retrieves a paginated collection of city data based on the specified pagination parameters.
        /// </summary>
        /// <param name="pagination">An object that specifies the pagination settings, including page size and page number. Must not be null.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of city
        /// data for the requested page. The collection is empty if no cities match the pagination criteria.</returns>
        Task<IEnumerable<CityData>> GetPaginated(CityDataPagination pagination);
        /// <summary>
        /// Asynchronously retrieves a collection of city data that matches the specified filter criteria.
        /// </summary>
        /// <param name="filter">An object that defines the filtering criteria to apply when selecting city data. Cannot be null.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of city
        /// data objects that satisfy the filter. The collection is empty if no cities match the criteria.</returns>
        Task<IEnumerable<CityData>> GetFiltered(CityDataFilter filter);
        /// <summary>
        /// Asynchronously retrieves city data for the specified city name.
        /// </summary>
        /// <param name="cityName">The name of the city to retrieve data for. Cannot be null or empty.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a <see cref="CityData"/> object
        /// for the specified city, or a default empty object if the city is not found.</returns>
        Task<CityData> GetByName(string cityName);
    }

    public class CityDataRepozitory : ICityDataRepozitory
    {
        readonly IInMemoryDb _db;
    
        public CityDataRepozitory(IInMemoryDb db)
        {
            _db = db;
        }

        public async Task<IEnumerable<CityData>> GetAll()
        {
            return _db.CitiesData.Select(c => c.Value);
        }

        public async Task<IEnumerable<CityData>> GetPaginated(CityDataPagination pagination)
        {
            var cities = await GetAll();

            if (pagination.PageNumber is not null && pagination.PageSize is not null)
            {
                cities = cities
                    .Skip((pagination.PageNumber.Value - 1) * pagination.PageSize.Value)
                    .Take(pagination.PageSize.Value);
            }

            return cities;
        }

        public async Task<IEnumerable<CityData>> GetFiltered(CityDataFilter filter)
        {
            var cities = await GetAll();

            if (filter.AverageGreaterThen is not null)
            {
                cities = cities.Where(c => c.AvgTemperature > filter.AverageGreaterThen);
            }

            if (filter.AverageLowerThen is not null)
            {
                cities = cities.Where(c => c.AvgTemperature < filter.AverageLowerThen);
            }

            return cities;
        }

        public async Task<CityData> GetByName(string cityName)
        {
            return _db.CitiesData.GetValueOrDefault(cityName, new(""));
        }
    }

    public class CityDataPagination
    {
        public int? PageSize { get; set; }
        public int? PageNumber { get; set; }
    }

    public class CityDataFilter
    {
        public double? AverageGreaterThen { get; set; }
        public double? AverageLowerThen { get; set; }
    }
}
