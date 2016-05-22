using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather
{
    /// <summary>
    /// WeatherServiceType contains the types of Weather data services this class can make
    /// </summary>
    public enum WeatherServiceType {OpenWeatherMap}

    /// <summary>
    /// Factory to create IWeatherDataServices
    /// </summary>
    public static class WeatherDataServiceFactory 
    {
        public static readonly WeatherServiceType OPEN_WEATHER_MAP = WeatherServiceType.OpenWeatherMap;

        /// <summary>
        /// Factory method
        /// </summary>
        /// <param name="type">The type of WeatherDataService desired</param>
        /// <returns>A class that implements IWeatherDataService</returns>
        public static IWeatherDataService GetWeatherDataService(WeatherServiceType type)
        {
            switch (type)
            {
                case WeatherServiceType.OpenWeatherMap:
                    return OpenMapDataService.Instance;

                default:
                   throw new WeatherDataServiceException("unsupported factory type");
            }
        }
    }
}
