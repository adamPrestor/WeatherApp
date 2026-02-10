namespace WeatherApp.ViewModels
{
    public class CityDataAverageTemperatureViewModel
    {
        public string Name { get; set; } = "";
        public double? AverageTemperature { get; set; }
    }

    public class CityDataViewModel : CityDataAverageTemperatureViewModel
    {
        public double? MaxTemperature { get; set; }
        public double? MinTemperature { get; set; }
    }
}
