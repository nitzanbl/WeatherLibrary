# WeatherLibrary

WeatherLibrary provides you with a standardized protocol for getting weather data for you apps.

## Instalation

To include WeatherLibrary into your project you can either download the repository and add a refference to the library from your project, or you can compile the library by itself and include a refference to the built dll file.

## Usage

To get the weather for today from a specific location:

```c#
OpenMapDataService.setApiKey("YOUR_OPEN_WEATHER_MAP_API_KEY");
IWeatherDataService weatherService = WeatherDataServiceFactory.GetWeatherDataService(WeatherDataServiceFactory.OPEN_WEATHER_MAP);

CityLocation location = new CityLocation("Tel Aviv");
WeatherData data = weatherService.GetWeatherData(location);
```

## Documentation

##### IWeatherDataService

`GetWeatherData` gets the current weather at the specified location optionally specifying if the result should be in celsius or fahrenheit (defaults to celsius if not specified).

```c#
WeatherData GetWeatherData(Location location, [WeatherUnits unit]);
```

`GetWeatherForcast` receives the same parameters as above except returns the forcasted weather for the next few days.

```c#
List<WeatherData> GetWeatherForcast(Location location, [WeatherUnits unit]);
```

`GetUVIndex` returns the current UV index as a float for the given location.

```c#
float GetUVIndex(Location location);
```

#####  Locations

In WeatherLibrary there are a number of ways you can specify a location:

###### City Location

Specify a location by city name with an optional second paramter for the countries code (e.g. IL for Israel). 

`CityLocation(string city, [string country_code])`

###### Coordinate Location

Specify a location via it's longitude and latitude

`CoordinateLocation(float lon, float lat)`

###### ZipLocation

Specify a location by a zip code and country code

`Ziplocation (string zip, string cCode)`

##### Weather Data Services

WeatherLibrary can be extended to include other weather APIs, but out of the box it comes with a data source for [openweathermap.org](http://openweathermap.org). To use the OpenWeatherMap data source you need to get an api key by signing up to their site, and then set the api key before calling weather functions by calling:

```c#
OpenMapDataService.setApiKey("YOUR_OPEN_WEATHER_MAP_API_KEY");
```

## Credits

WeatherLibrary was created by Nitzan Blankleder and Gabriel Manricks

