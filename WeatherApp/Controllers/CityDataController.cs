using Microsoft.AspNetCore.Mvc;
using WeatherApp.Models;

namespace WeatherApp.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CityDataController : ControllerBase
    {
        readonly IInMemoryDb _db;

        public CityDataController(IInMemoryDb db)
        {
            _db = db;
        }

        [HttpGet]
        async public Task<IEnumerable<CityDataViewModel>> List([FromQuery] CityDataPagination filter)
        {
            var cities = _db.CitiesData.Select(c => c.Value);

            if (filter.PageNumber is not null && filter.PageSize is not null)
            {
                cities = cities
                    .Skip((filter.PageNumber.Value - 1) * filter.PageSize.Value)
                    .Take(filter.PageSize.Value);
            }

            return cities.Select(c => c.ToViewModel());
        }

        [HttpGet("{name}")]
        async public Task<CityDataViewModel?> Get(string name)
        {
            CityData city = _db.CitiesData.GetValueOrDefault(name, new(""));

            return city?.ToViewModel();
        }

        [HttpGet("Average")]
        async public Task<IEnumerable<CityDataAverageTemperatureViewModel>> GetListOfAverages(
            [FromQuery] AverageCityDataFilter filter)
        {
            var cities = _db.CitiesData.Select(c => c.Value);

            if (filter.GreaterThen is not null)
                cities = cities.Where(c => c.AvgTemperature > filter.GreaterThen);

            if (filter.LowerThen is not null)
                cities = cities.Where(c => c.AvgTemperature < filter.LowerThen);

            return cities.Select(c => c.ToAverageTemperatureViewModel());
        }
    }

    public class CityDataPagination
    {
        public int? PageSize { get; set; }
        public int? PageNumber { get; set; }
    }

    public class AverageCityDataFilter
    {
        public double? GreaterThen { get; set; }
        public double? LowerThen { get; set; }
    }
}
