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
        async public Task<IEnumerable<CityViewModel>> List()
        {
            return _db.Cities.Select(c => c.Value.ToViewModel());
        }

        [HttpGet("{name}")]
        async public Task<CityViewModel?> Get(string name)
        {
            var city = _db.Cities.GetValueOrDefault(name);
            return city?.ToViewModel();
        }

        [HttpGet("average")]
        async public Task<IEnumerable<CityAverageTemperatureViewModel>> GetListOfAverages(
            [FromQuery] double? largerThan,
            [FromQuery] double? smallerThan)
        {
            var cities = _db.Cities.Select(c => c.Value);

            if (largerThan is not null)
            {
                cities = cities.Where(c => c.AverageTemperature > largerThan);
            }

            if (smallerThan is not null)
            {
                cities = cities.Where(c => c.AverageTemperature < smallerThan);
            }

            return cities.Select(c => c.ToAverageTemperatureViewModel());
        }
    }
}
