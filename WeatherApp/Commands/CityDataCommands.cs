namespace WeatherApp.Commands
{
    public class CityDataPaginationCommand
    {
        public int? PageSize { get; set; }
        public int PageNumber { get; set; }
    }

    public class CityDataFilterCommand
    {
        public double? AverageGreaterThen { get; set; }
        public double? AverageLowerThen { get; set; }
    }
}
