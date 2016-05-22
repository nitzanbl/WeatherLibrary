using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather
{
    /// <summary>
    /// CityLocation represents a location via City name and Country Code
    /// </summary>
    public class CityLocation : Location
    {
        public string cityName { get; set; }
        public string countryCode { get; set; }

        /// <summary>
        /// CityLocation constructor
        /// </summary>
        /// <param name="city">City name</param>
        /// <param name="code">Country code</param>
        public CityLocation(string city, string code = null)
        {
            this.cityName = city;
            this.countryCode = code;
        }

    }
}
