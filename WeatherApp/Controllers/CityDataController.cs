using Microsoft.AspNetCore.Mvc;
using WeatherApp.Models;
using WeatherApp.Repozitories;

namespace WeatherApp.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CityDataController : ControllerBase
    {
        readonly ICityDataRepozitory _cityDataRepozitory;

        public CityDataController(ICityDataRepozitory cityDataRepozitory)
        {
            _cityDataRepozitory = cityDataRepozitory;
        }

        [HttpGet]
        async public Task<IEnumerable<CityDataViewModel>> List([FromQuery] CityDataPagination filter)
        {
            return (await _cityDataRepozitory.GetPaginated(filter)).Select(c => c.ToViewModel());
        }

        [HttpGet("{name}")]
        async public Task<CityDataViewModel> Get(string name)
        {
            return (await _cityDataRepozitory.GetByName(name)).ToViewModel();
        }

        [HttpGet("Average")]
        async public Task<IEnumerable<CityDataAverageTemperatureViewModel>> GetListOfAverages(
            [FromQuery] CityDataFilter filter)
        {
            return (await _cityDataRepozitory.GetFiltered(filter)).Select(c => c.ToAverageTemperatureViewModel());
        }
    }    
}
