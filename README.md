# WeatherApp

A simple app that exposes city's temperature data based on the input file. It exposes three endpoints:

- `api/v1/CityData` returns a list of all cities and their max, min and average temperature.
- `api/v1/CityData/{name}` returns a max, min and average temperature for city with the specified name. If city is not found endpoint returns a new object with default values. Default values for temperatures are `null`
- `api/v1/CityData/Average` returns a name and average temperature for all cities that meet the critiria. Cities can be filtered by min or max average temperature.

Since input is file based app also exposes a recalculate endpoint:

- `/api/v1/Measurement/Recalculate` force recalculation of weather data manually.

> #### Note
>
> We also tried to implement a `SystemFileWatcher` solution, to detect changes in a file to automatically run recalculation. It works fine for small files, but fails for larger files.

## Data File

App expects an input file in `WeatherApp/Assets/measurements.txt`. If file is not provided, then system operates with empty data.
