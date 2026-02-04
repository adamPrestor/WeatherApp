using Microsoft.AspNetCore.Mvc;
using WeatherApp.Models;

namespace WeatherApp.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        readonly IInMemoryDb _db;

        public CityController(IInMemoryDb db)
        {
            _db = db;
        }

        [HttpGet]
        async public Task<IEnumerable<CityDataViewModel>> List()
        {
            return _db.CitiesData.Select(c => c.Value.ToViewModel());
        }

        [HttpGet("{name}")]
        async public Task<CityDataViewModel?> Get(string name)
        {
            var city = _db.CitiesData.GetValueOrDefault(name);
            return city?.ToViewModel();
        }

        [HttpGet("average")]
        async public Task<IEnumerable<CityDataAverageTemperatureViewModel>> GetListOfAverages(
            [FromQuery] double? largerThan,
            [FromQuery] double? smallerThan)
        {
            var cities = _db.CitiesData.Select(c => c.Value);

            if (largerThan is not null)
            {
                cities = cities.Where(c => c.AvgTemperature > largerThan);
            }

            if (smallerThan is not null)
            {
                cities = cities.Where(c => c.AvgTemperature < smallerThan);
            }

            return cities.Select(c => c.ToAverageTemperatureViewModel());
        }
    }
}
