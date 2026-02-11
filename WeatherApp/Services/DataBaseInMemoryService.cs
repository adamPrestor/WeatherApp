namespace WeatherApp.Services
{
    public class DataBaseInMemoryService : IDataBaseService
    {
        readonly IInMemoryDb _db;

        public DataBaseInMemoryService(IInMemoryDb db)
        {
            _db = db;
        }

        public async Task Fetch()
        {
            await _db.Fetch();
        }
    }
}
