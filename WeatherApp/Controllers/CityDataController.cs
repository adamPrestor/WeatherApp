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
            var city = _db.CitiesData.GetValueOrDefault(name);

            return city?.ToViewModel();
        }

        [HttpGet("average")]
        async public Task<IEnumerable<CityDataAverageTemperatureViewModel>> GetListOfAverages(
            [FromQuery] AverageCityDataFilter filter)
        {
            var cities = _db.CitiesData.Select(c => c.Value);

            if (filter.LargerThan is not null)
                cities = cities.Where(c => c.AvgTemperature > filter.LargerThan);

            if (filter.SmallerThan is not null)
                cities = cities.Where(c => c.AvgTemperature < filter.SmallerThan);

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
        public double? LargerThan { get; set; }
        public double? SmallerThan { get; set; }
    }
}
