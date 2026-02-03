
using WeatherApp.Models;

namespace WeatherApp.Measurements
{
    public class MeasurementHostedService : IHostedService
    {
        readonly IInMemoryDb _db;

        public MeasurementHostedService(IInMemoryDb db)
        {
            _db = db;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Initializing database...");
            await _db.Fetch();
            Console.WriteLine("Finished initializing database...");
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
