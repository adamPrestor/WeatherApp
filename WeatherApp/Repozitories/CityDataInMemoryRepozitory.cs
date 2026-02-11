using WeatherApp.Commands;
using WeatherApp.Models;

namespace WeatherApp.Repozitories
{
    public class CityDataInMemoryRepozitory : ICityDataRepozitory
    {
        readonly IInMemoryDb _db;
    
        public CityDataInMemoryRepozitory(IInMemoryDb db)
        {
            _db = db;
        }

        public async Task<IEnumerable<CityData>> GetAll()
        {
            return _db.CitiesData.Select(c => c.Value);
        }

        public async Task<IEnumerable<CityData>> GetPaginated(CityDataPaginationCommand pagination)
        {
            var cities = await GetAll();

            if (pagination.PageSize is not null)
            {
                cities = cities
                    .Skip((pagination.PageNumber - 1) * pagination.PageSize.Value)
                    .Take(pagination.PageSize.Value);
            }

            return cities;
        }

        public async Task<IEnumerable<CityData>> GetFiltered(CityDataFilterCommand filter)
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
}
