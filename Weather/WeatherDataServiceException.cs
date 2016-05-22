using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather
{
    /// <summary>
    /// WeatherDataServiceException is a library exception class
    /// </summary>
    class WeatherDataServiceException : Exception
    {
        public WeatherDataServiceException()
        {
        }

        public WeatherDataServiceException(string message) :base(message)
        {
        }

        public WeatherDataServiceException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
