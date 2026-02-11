namespace WeatherApp.Services
{
    public interface IDataBaseService
    {
        /// <summary>
        /// Initiates an asynchronous fetch operation.
        /// </summary>
        /// <returns>A task that represents the asynchronous fetch operation.</returns>
        Task Fetch();
    }
}
