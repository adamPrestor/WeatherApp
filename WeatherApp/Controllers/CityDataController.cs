using Microsoft.AspNetCore.Mvc;
using WeatherApp.Adapters;
using WeatherApp.Models;
using WeatherApp.Repozitories;

namespace WeatherApp.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CityDataController : ControllerBase
    {
        readonly ICityDataAdapter _cityDataAdapter;
        readonly ICityDataRepozitory _cityDataRepozitory;

        public CityDataController(ICityDataAdapter cityDataAdapter, ICityDataRepozitory cityDataRepozitory)
        {
            _cityDataRepozitory = cityDataRepozitory;
            _cityDataAdapter = cityDataAdapter;
        }

        [HttpGet]
        async public Task<IEnumerable<CityDataViewModel>> List([FromQuery] CityDataPagination filter)
        {
            var cities = await _cityDataRepozitory.GetPaginated(filter);
            return _cityDataAdapter.ToViewModel(cities);
        }

        [HttpGet("{name}")]
        async public Task<CityDataViewModel> Get(string name)
        {
            var cities = await _cityDataRepozitory.GetByName(name);
            return _cityDataAdapter.ToViewModel(cities);
        }

        [HttpGet("Average")]
        async public Task<IEnumerable<CityDataAverageTemperatureViewModel>> GetListOfAverages(
            [FromQuery] CityDataFilter filter)
        {
            var cities = await _cityDataRepozitory.GetFiltered(filter);
            return _cityDataAdapter.ToAverageTemperatureViewModel(cities);
        }
    }    
}
