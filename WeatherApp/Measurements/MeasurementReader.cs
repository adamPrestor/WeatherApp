using System.Diagnostics;
using System.Globalization;
using WeatherApp.Models;

namespace WeatherApp.Measurements
{
    public class MeasurementReader
    {
        private static readonly string _assetsPath = "Assets";
        private static readonly string _filePath = $"measurements.txt";

        public static string AssetsPath { get => Path.GetFullPath($"{Directory.GetCurrentDirectory()}/{_assetsPath}");  }
        public static string FilePath { get => Path.GetFullPath($"{AssetsPath}/{_filePath}"); }

        public static Dictionary<string, CityData>? Read()
        {
            var cities = new Dictionary<string, CityData>();

            string filePath = FilePath;

            Debug.WriteLine($"Reading measurements from: {filePath}");
            Stopwatch stopwatch = Stopwatch.StartNew();

            FileStream? stream;

            try
            {
                stream = new FileStream(
                    filePath,
                    FileMode.Open,
                    FileAccess.Read,
                    FileShare.Read,
                    bufferSize: 4096,
                    useAsync: true);
            } catch (Exception ex)
            {
                Debug.WriteLine($"Error opening file: {ex.Message}");
                return null;
            }

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
                        CityData cityData = new(city);

                        existingCity = cities[city] = cityData;
                    }

                    existingCity.AddTemperature(temperature);
                }
            }

            foreach(var city in cities.Values)
            {
                city.SetAverage();
            }

            // Debug utility to measure reading time
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);

            return cities;
        }

        public static async Task<Dictionary<string, CityData>?> ReadAsync()
        {
            return await Task.Run(() => Read());
        }
    }
}