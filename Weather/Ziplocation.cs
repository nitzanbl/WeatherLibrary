using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather
{
    /// <summary>
    /// Ziplocation represents a location via a zip code and country code
    /// </summary>
    public class Ziplocation : Location
    {
        public string zipCode { get; set; }
        public string countryCode { get; set; }

        /// <summary>
        /// ZipLocation constructor
        /// </summary>
        /// <param name="zip">Zip code</param>
        /// <param name="cCode">Country code</param>
        public Ziplocation (string zip, string cCode)
        {
            this.countryCode = cCode;
            this.zipCode = zip;
        }
    }
}
