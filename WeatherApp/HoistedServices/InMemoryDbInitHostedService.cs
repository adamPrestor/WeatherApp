using WeatherApp.Services;

namespace WeatherApp.HoistedServices
{
    public class InMemoryDbInitHostedService : IHostedService
    {
        readonly IDataBaseService _dataBaseService;

        public InMemoryDbInitHostedService(IDataBaseService dataBaseService)
        {
            _dataBaseService = dataBaseService;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Initializing database...");
            await _dataBaseService.Fetch();
            Console.WriteLine("Finished initializing database...");
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
        }
    }
}
