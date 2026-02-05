using Microsoft.AspNetCore.Mvc;
using WeatherApp.Models;

namespace WeatherApp.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MeasurementController : ControllerBase
    {
        readonly IInMemoryDb _db;

        public MeasurementController(IInMemoryDb db)
        {
            _db = db;
        }

        [HttpGet("Recalculate")]
        async public Task Recalculate() 
        {
            await _db.Fetch();
        }
    }
}
