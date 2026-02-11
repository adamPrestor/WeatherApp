using WeatherApp.Commands;
using WeatherApp.Requests;

namespace WeatherApp.Validators
{
    public interface ICityDataValidator
    {
        /// <summary>
        /// Validates the specified pagination request and returns a command representing the validated parameters.
        /// </summary>
        /// <remarks>If the request contains invalid pagination parameters, such as negative page numbers
        /// or page sizes, an exception may be thrown. Ensure that the request object is properly populated before
        /// calling this method.</remarks>
        /// <param name="request">The pagination request to validate. Cannot be null. The request should contain valid page number and page
        /// size values.</param>
        /// <returns>A CityDataPaginationCommand containing the validated pagination parameters. Throws an exception if the
        /// request is invalid.</returns>
        CityDataPaginationCommand ValidatePaginationRequest(CityDataPaginationRequest request);

        /// <summary>
        /// Validates the specified city data filter request and returns a command representing the validated filter
        /// criteria.
        /// </summary>
        /// <param name="request">The filter request to validate. Cannot be null.</param>
        /// <returns>A CityDataFilterCommand containing the validated filter criteria derived from the request.</returns>
        CityDataFilterCommand ValidateFilterRequest(CityDataFilterRequest request);
    }

    public class CityDataValidator : ICityDataValidator
    {
        public CityDataPaginationCommand ValidatePaginationRequest(CityDataPaginationRequest request)
        {
            if (request.PageNumber < 1)
            {
                throw new ArgumentException("Page number must be greater than `0`.");
            }

            if (request.PageSize < 1)
            {
                throw new ArgumentException("Page size must be greater then `0`.");
            }

            return new()
            {
                PageNumber = request.PageNumber ?? 1,
                PageSize = request.PageSize
            };
        }

        public CityDataFilterCommand ValidateFilterRequest(CityDataFilterRequest request)
        {
            if (
                request.AverageGreaterThen is not null &&
                request.AverageLowerThen is not null &&
                request.AverageGreaterThen >= request.AverageLowerThen
                )
            {
                throw new ArgumentException("`AverageGreaterThen must be less lower then `AverageLowerThen`.");
            }

            return new()
            {
                AverageGreaterThen = request.AverageGreaterThen,
                AverageLowerThen = request.AverageLowerThen
            };
        }
    }
}
