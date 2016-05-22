using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather
{
    /// <summary>
    /// CoordinateLocation represents a location via a Longitude and latitude
    /// </summary>
    public class CoordinateLocation : Location
    {
        public float lon { get; set; }
        public float lat { get; set; }

        /// <summary>
        /// CoordinateLocation Constructor
        /// </summary>
        /// <param name="lon">The Longitude</param>
        /// <param name="lat">The Latitude</param>
        public CoordinateLocation(float lon, float lat)
        {
            this.lat = lat;
            this.lon = lon;
        }
    }
}
