using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather
{
    /// <summary>
    /// WeatherUnits represents the temperatur unit
    /// </summary>
    public enum WeatherUnits {Celsius, Fahrenheit, Kelvin};

    /// <summary>
    /// Interface for this libraries API 
    /// </summary>
    public interface IWeatherDataService
    {
        /// <summary>
        /// GetWeatherData returns the current wether for a specified location
        /// </summary>
        /// <param name="location">The location for which to retrieve the weather</param>
        /// <param name="unit">The temperature unit to use (Default: Celsius)</param>
        /// <returns></returns>
        WeatherData GetWeatherData(Location location, WeatherUnits unit = WeatherUnits.Celsius);

        /// <summary>
        /// GetWeatherForcast returns a list of forcasted weather forcasts for a given location
        /// </summary>
        /// <param name="location">The location for which to retrieve the weather</param>
        /// <param name="unit">The temperature unit to use (Default: Celsius)</param>
        /// <returns></returns>
        List<WeatherData> GetWeatherForcast(Location location, WeatherUnits unit = WeatherUnits.Celsius);

        /// <summary>
        /// Get the current UV index for a location
        /// </summary>
        /// <param name="location">The location for which to retrieve the UV index</param>
        /// <returns></returns>
        float GetUVIndex(Location location);
    }
}
