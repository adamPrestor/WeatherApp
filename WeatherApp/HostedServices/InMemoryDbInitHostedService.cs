using WeatherApp.Services;

namespace WeatherApp.HostedServices
{
    public class InMemoryDbInitHostedService : IHostedService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public InMemoryDbInitHostedService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Initializing database...");
            using var scope = _scopeFactory.CreateScope();

            var dataBaseService = scope.ServiceProvider.GetService<IDataBaseService>();

            if (dataBaseService is not null)
            {
                await dataBaseService.Fetch();
            }
            Console.WriteLine("Finished initializing database...");
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
        }
    }
}
