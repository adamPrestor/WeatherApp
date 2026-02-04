namespace WeatherApp.Models
{
    public class City
    {
        public string Name { get; private set; }
        private double TemperatureSum { get; set; }
        private int TemperatureCount { get; set; }
        public double AverageTemperature { get; private set; }
        public double MaxTemperature { get; private set; } = double.MinValue;
        public double MinTemperature { get; private set; } = double.MaxValue;

        public City(string name)
        {
            Name = name;
        }

        public City(string name, double temperature)
        {
            Name = name;
            AddTemperature(temperature);
        }

        public void AddTemperature(double temperature)
        {
            TemperatureSum += temperature;
            TemperatureCount++;

            //AverageTemperature = TemperatureSum / TemperatureCount;

            if (temperature > MaxTemperature)
            {
                MaxTemperature = temperature;
            }

            if (temperature < MinTemperature)
            {
                MinTemperature = temperature;
            }
        }

        public void SetAverage()
        {
            AverageTemperature = TemperatureSum / TemperatureCount;
        }
    }

    public class CityAverageTemperatureViewModel
    {
        public string Name { get; set; } = "";
        public double AverageTemperature { get; set; }
    }

    public class CityViewModel : CityAverageTemperatureViewModel
    {
        public double MaxTemperature { get; set; }
        public double MinTemperature { get; set; }
    }

    public static class CityExtensions
    {
        public static CityViewModel ToViewModel(this City model)
        {
            return new()
            {
                Name = model.Name,  
                AverageTemperature = model.AverageTemperature,
                MinTemperature = model.MinTemperature,
                MaxTemperature = model.MaxTemperature,
            };
        }

        public static CityAverageTemperatureViewModel ToAverageTemperatureViewModel(this City model)
        {
            return new()
            {
                Name = model.Name,
                AverageTemperature = model.AverageTemperature
            };
        }
    }
}
