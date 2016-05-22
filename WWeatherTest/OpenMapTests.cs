using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Weather;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WWeatherTest
{
    [TestClass]
    public class OpenMapTests
    {
        internal class MockOpenMapDataService : OpenMapDataService
        {
            public MockOpenMapDataService() : base()
            {
            }

           override protected Stream MakeWebRequest(String url)
            {
                String path;
                if (url.StartsWith("http://api.openweathermap.org/data/2.5/forecast"))
                {
                    path = "MockData/forcast.json";
                }
                else if (url.StartsWith("http://api.openweathermap.org/data/2.5/weather"))
                {
                    path = "MockData/weather.json";
                }
                else if (url.StartsWith("http://api.openweathermap.org/v3/uvi"))
                {
                    path = "MockData/uv.json";
                }
                else
                {
                    Assert.Fail("Undesired Web request made");
                    return null;
                }
                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs);
                string forcast = sr.ReadToEnd();
                MemoryStream ms = new MemoryStream();
                StreamWriter sw = new StreamWriter(ms);
                sw.Write(forcast);
                sw.Flush();
                ms.Position = 0;
                return ms;
            }

        }
        [TestMethod]
        public void TestGetWeatherForcast()
        {
            String apikey = "547974e171d5152e1889f080d28b1b0f";
            OpenMapDataService.setApiKey(apikey);

            Location location = new CityLocation("Tel Aviv");

            IWeatherDataService weather = new MockOpenMapDataService();
            List<WeatherData> data = weather.GetWeatherForcast(location);

            Assert.AreEqual(2, data.Count);
            Assert.AreEqual(19.28, data[0].temp, 0.01);
            Assert.AreEqual(location, data[0].location);
            Assert.AreEqual(WeatherUnits.Celsius, data[0].unit);
            Assert.AreEqual(1.96, data[0].windSpeed, 0.01);
        }

        [TestMethod]
        public void TestGetWeather()
        {
            String apikey = "547974e171d5152e1889f080d28b1b0f";
            OpenMapDataService.setApiKey(apikey);

            Location location = new CityLocation("Tel Aviv");

            IWeatherDataService weather = new MockOpenMapDataService();
            WeatherData data = weather.GetWeatherData(location);

            Assert.AreEqual(18.30, data.temp, 0.01);
            Assert.AreEqual(location, data.location);
            Assert.AreEqual(WeatherUnits.Celsius, data.unit);
            Assert.AreEqual(1.80, data.windSpeed, 0.01);
        }

        [TestMethod]
        public void TestGetUVIndex()
        {
            String apikey = "547974e171d5152e1889f080d28b1b0f";
            OpenMapDataService.setApiKey(apikey);

            Location location = new CoordinateLocation(30.75f, 33.25f);

            IWeatherDataService weather = new MockOpenMapDataService();
            float data = weather.GetUVIndex(location);

            Assert.AreEqual(9.3, data, 0.01);
        }
    }
}
