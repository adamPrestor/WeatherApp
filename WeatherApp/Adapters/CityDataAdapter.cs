using WeatherApp.Models;

namespace WeatherApp.Adapters
{
    public interface ICityDataAdapter
    {
        /// <summary>
        /// Converts the specified city data to a view model suitable for presentation in the user interface.
        /// </summary>
        /// <param name="cityData">The city data to convert. Cannot be null.</param>
        /// <returns>A view model representing the provided city data.</returns>
        CityDataViewModel ToViewModel(CityData cityData);

        /// <summary>
        /// Converts a collection of city data entities to their corresponding view model representations.
        /// </summary>
        /// <param name="cityDataEnumerable">The collection of city data entities to convert. Cannot be null.</param>
        /// <returns>An enumerable collection of view models representing the provided city data entities. The collection will be
        /// empty if the input contains no elements.</returns>
        IEnumerable<CityDataViewModel> ToViewModel(IEnumerable<CityData> cityDataEnumerable);

        /// <summary>
        /// Creates a view model representing the average temperature data for a city based on the provided city
        /// information.
        /// </summary>
        /// <param name="cityData">The city data containing temperature and related information to be converted into a view model. Cannot be
        /// null.</param>
        /// <returns>A view model containing the average temperature information for the specified city.</returns>
        CityDataAverageTemperatureViewModel ToAverageTemperatureViewModel(CityData cityData);

        /// <summary>
        /// Transforms a collection of city temperature data into view models representing the average temperature for
        /// each city.
        /// </summary>
        /// <param name="cityData">A collection of city temperature data to be converted. Cannot be null.</param>
        /// <returns>An enumerable collection of view models containing the average temperature information for each city. The
        /// collection will be empty if no city data is provided.</returns>
        IEnumerable<CityDataAverageTemperatureViewModel> ToAverageTemperatureViewModel(IEnumerable<CityData> cityData);
    }

    public class CityDataAdapter : ICityDataAdapter
    {
        public CityDataViewModel ToViewModel(CityData cityData)
        {
            return cityData.ToViewModel();
        }

        public IEnumerable<CityDataViewModel> ToViewModel(IEnumerable<CityData> cityDataEnumerable)
        {
            return cityDataEnumerable.Select(ToViewModel);
        }

        public CityDataAverageTemperatureViewModel ToAverageTemperatureViewModel(CityData cityData)
        {
            return cityData.ToAverageTemperatureViewModel();
        }

        public IEnumerable<CityDataAverageTemperatureViewModel> ToAverageTemperatureViewModel(IEnumerable<CityData> cityData)
        {
            return cityData.Select(ToAverageTemperatureViewModel);
        }
    }
}
