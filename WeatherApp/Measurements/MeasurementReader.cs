using System.Diagnostics;
using System.Globalization;
using WeatherApp.Models;

namespace WeatherApp.Measurements
{
    public class MeasurementReader
    {
        private static readonly string _filePath = "Assets/measurements.txt";

        private static string FilePath()
        {
            string root = Directory.GetCurrentDirectory();
            return $"{root}/{_filePath}";
        }

        public static Dictionary<string, City> Read()
        {
            var cities = new Dictionary<string, City>();

            string filePath = FilePath();

            Debug.WriteLine($"Reading measurements from: {filePath}");
            Stopwatch stopwatch = Stopwatch.StartNew();

            using var stream = new FileStream(
                filePath,
                FileMode.Open,
                FileAccess.Read,
                FileShare.Read,
                bufferSize: 4096,
                useAsync: true);

            using var reader = new StreamReader(stream);

            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                // Example parsing
                var tokens = line.Split(';', 2);

                if (tokens.Length == 2)
                {
                    var (city, temperature) = (
                        tokens[0],
                        double.Parse(tokens[1], CultureInfo.InvariantCulture));

                    var existingCity = cities.GetValueOrDefault(city);

                    if (existingCity is null)
                    {
                        City cityData = new(city);

                        existingCity = cities[city] = cityData;
                    }

                    existingCity.AddTemperature(temperature);
                }
            }

            foreach(var city in cities.Values)
            {
                city.SetAverage();
            }

            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);

            return cities;
        }

        public static async Task<Dictionary<string, City>> ReadAsync()
        {
            return await Task.Run(() => Read());
        }
    }
}