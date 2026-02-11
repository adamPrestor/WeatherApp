namespace WeatherApp.Requests
{
    public class CityDataPaginationRequest
    {
        public int? PageSize { get; set; }
        public int? PageNumber { get; set; }
    }

    public class CityDataFilterRequest
    {
        public double? AverageGreaterThen { get; set; }
        public double? AverageLowerThen { get; set; }
    }
}
