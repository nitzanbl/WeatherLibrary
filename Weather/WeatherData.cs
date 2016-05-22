using System;
namespace Weather
{
    /// <summary>
    /// WeatherData represents the weather at a given time
    /// </summary>
    public class WeatherData
    {
        public WeatherUnits unit { get; set; }
        public Location location { get; set; }
        public float temp { get; set; }
        public int humidity { get; set; }
        public float windSpeed { get; set; }
        public int clouds { get; set; }
        public string description { get; set; }
        public string status { get; set; }
        public DateTime date { get; set; }
    }
}