using Microsoft.AspNetCore.Mvc;
using WeatherApp.Services;

namespace WeatherApp.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MeasurementController : ControllerBase
    {
        readonly IDataBaseService _dataBaseService;

        public MeasurementController(IDataBaseService dataBaseService)
        {
            _dataBaseService = dataBaseService;
        }

        [HttpGet("Recalculate")]
        async public Task Recalculate() 
        {
            await _dataBaseService.Fetch();
        }
    }
}
