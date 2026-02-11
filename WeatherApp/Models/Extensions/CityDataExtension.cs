using WeatherApp.ViewModels;

namespace WeatherApp.Models.Extensions
{
    public static class CityDataExtension
    {
        public static CityDataViewModel ToViewModel(this CityData model)
        {
            return new()
            {
                Name = model.Name,
                AverageTemperature = model.AvgTemperature,
                MinTemperature = model.MinTemperature,
                MaxTemperature = model.MaxTemperature,
            };
        }

        public static CityDataAverageTemperatureViewModel ToAverageTemperatureViewModel(this CityData model)
        {
            return new()
            {
                Name = model.Name,
                AverageTemperature = model.AvgTemperature
            };
        }
    }
}
