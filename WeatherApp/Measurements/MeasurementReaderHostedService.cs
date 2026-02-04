namespace WeatherApp.Measurements
{
    public class MeasurementReaderHostedService : IHostedService
    {
        readonly IInMemoryDb _db;
        FileSystemWatcher? _watcher;

        public MeasurementReaderHostedService(IInMemoryDb db)
        {
            _db = db;
        }

        private async void OnChanged(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType != WatcherChangeTypes.Changed)
                return;

            Console.WriteLine($"File changed: {e.FullPath}");

            // Path.GetFullPath is just fail safe mechanism, might be obsolete
            if (Path.GetFullPath(e.FullPath) == Path.GetFullPath(MeasurementReader.FilePath))
            {
                // TODO: does not work for BIG files - file is still locked by OS
                await _db.Fetch();
            }
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            // https://learn.microsoft.com/en-us/dotnet/api/system.io.filesystemwatcher?view=net-10.0
            _watcher = new FileSystemWatcher(MeasurementReader.AssetsPath);

            Console.WriteLine($"Watching directory: {MeasurementReader.AssetsPath}");
            _watcher.Changed += OnChanged;

            _watcher.EnableRaisingEvents = true;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _watcher?.Dispose();
        }
    }
}
