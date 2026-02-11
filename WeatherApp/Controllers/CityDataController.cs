using Microsoft.AspNetCore.Mvc;

using WeatherApp.Adapters;
using WeatherApp.Repozitories;
using WeatherApp.Requests;
using WeatherApp.Validators;
using WeatherApp.ViewModels;

namespace WeatherApp.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CityDataController : ControllerBase
    {
        readonly ICityDataAdapter _cityDataAdapter;
        readonly ICityDataRepozitory _cityDataRepozitory;
        readonly ICityDataValidator _cityDataValidator;

        public CityDataController(ICityDataAdapter cityDataAdapter,
            ICityDataRepozitory cityDataRepozitory,
            ICityDataValidator cityDataValidator)
        {
            _cityDataRepozitory = cityDataRepozitory;
            _cityDataAdapter = cityDataAdapter;
            _cityDataValidator = cityDataValidator;
        }

        [HttpGet]
        async public Task<IEnumerable<CityDataViewModel>> List([FromQuery] CityDataPaginationRequest pagination)
        {
            var cmd = _cityDataValidator.ValidatePaginationRequest(pagination);
            var cities = await _cityDataRepozitory.GetPaginated(cmd);
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
            [FromQuery] CityDataFilterRequest filter)
        {
            var cmd = _cityDataValidator.ValidateFilterRequest(filter);
            var cities = await _cityDataRepozitory.GetFiltered(cmd);
            return _cityDataAdapter.ToAverageTemperatureViewModel(cities);
        }
    }    
}
