using WeatherApp.ViewModels;

namespace WeatherApp.Models
{
    public class CityData
    {
        public string Name { get; private set; }
        private double TemperatureSum { get; set; }
        private int TemperatureCount { get; set; }
        public double? AvgTemperature { get; private set; }
        public double? MaxTemperature { get; private set; }
        public double? MinTemperature { get; private set; }

        public CityData(string name)
        {
            Name = name;
        }

        public CityData(string name, double temperature)
        {
            Name = name;
            AddTemperature(temperature);
        }

        public void AddTemperature(double temperature)
        {
            TemperatureSum += temperature;
            TemperatureCount++;

            //AverageTemperature = TemperatureSum / TemperatureCount;

            if (MaxTemperature is null || temperature > MaxTemperature)
            {
                MaxTemperature = temperature;
            }

            if (MinTemperature is null || temperature < MinTemperature)
            {
                MinTemperature = temperature;
            }
        }

        public void SetAverage()
        {
            AvgTemperature = TemperatureSum / TemperatureCount;
        }
    }
}
