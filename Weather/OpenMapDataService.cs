using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace Weather
{
    public class OpenMapDataService : IWeatherDataService
    {
        private const string BaseURL = "http://api.openweathermap.org";
        private static string APIKey = null;
        private static OpenMapDataService instance;

        public static void setApiKey(String apiKey)
        {
            APIKey = apiKey;
        }
        protected OpenMapDataService()
        {
            if (APIKey == null)
            {
                throw new WeatherDataServiceException("API wasn't set");
            }
        }

        public static OpenMapDataService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OpenMapDataService();
                }
                return instance;
            }
        }

        public float GetUVIndex(Location location)
        {
            if(location is CoordinateLocation)
            {
                CoordinateLocation loc = (CoordinateLocation) location;
                string url = BaseURL + "/v3/uvi/" + 
                    loc.lat + "," + loc.lon + "/current.json?appid=" + APIKey;
                
                DataContractJsonSerializer parser = new DataContractJsonSerializer(typeof(UVJson));
                UVJson json = (UVJson)parser.ReadObject((MakeWebRequest(url)));
                return json.data;
            }
            else
            {
                throw new WeatherDataServiceException("only CoordinateLocation is supported to get UV index");
            }
          

        }
        
        private WeatherData FromJsonToData(WeatherJson json, Location location, WeatherUnits unit)
        {
            WeatherData data = new WeatherData();
            data.date = new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime().AddSeconds(json.dt);
            data.unit = unit;
            data.location = location;
            data.humidity = json.main.humidity;
            data.temp = json.main.temp;
            data.windSpeed = json.wind.speed;
            data.clouds = json.clouds.all;
            data.status = json.weather.First().main;
            data.description = json.weather.First().description;
            return data;
        } 

        public WeatherData GetWeatherData(Location location, WeatherUnits unit)
        {
           string url = BaseURL + "/data/2.5/weather?";
           if (location is CityLocation)
            {
                CityLocation loc = (CityLocation)location;
                url += "q=" + loc.cityName;
                if(!System.String.IsNullOrEmpty(loc.countryCode))
                {
                    url += "," + loc.countryCode;
                }
            }
           else if(location is Ziplocation)
            {
                Ziplocation loc = (Ziplocation)location;
                url += "zip=" + loc.zipCode + "," + loc.countryCode;
            }
           else if(location is CoordinateLocation)
            {
                CoordinateLocation loc = (CoordinateLocation)location;
                url += "lat=" + loc.lat + "&lon=" + loc.lon;

            }
           else
            {
                throw new WeatherDataServiceException("unsupported location type");
            }
            if(unit == WeatherUnits.Celsius)
            {
                url += "&units=metric";
            }
            else if(unit == WeatherUnits.Fahrenheit)
            {
                url += "&units=imperial";
            }
            else if(unit == WeatherUnits.Kelvin)
            {

            }
            else
            {
                throw new WeatherDataServiceException("unsupported unit type");
            }
            url += "&appid=" + APIKey;
            
            DataContractJsonSerializer parser = new DataContractJsonSerializer(typeof(WeatherJson));
            WeatherJson json = (WeatherJson)parser.ReadObject(MakeWebRequest(url));
            return FromJsonToData(json, location, unit);
        }

        virtual protected Stream MakeWebRequest(String url)
        {
            HttpWebRequest req = WebRequest.CreateHttp(url);
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            if (resp.StatusCode != HttpStatusCode.OK)
            {
                throw new WeatherDataServiceException("network error getting weather data");
            }
            return resp.GetResponseStream();
        }

        public List<WeatherData> GetWeatherForcast(Location location, WeatherUnits unit)
        {
            string url = BaseURL + "/data/2.5/forecast?";
            if (location is CityLocation)
            {
                CityLocation loc = (CityLocation)location;
                url += "q=" + loc.cityName;
                if (!System.String.IsNullOrEmpty(loc.countryCode))
                {
                    url += "," + loc.countryCode;
                }
            }
            else if (location is CoordinateLocation)
            {
                CoordinateLocation loc = (CoordinateLocation)location;
                url += "lat=" + loc.lat + "&lon=" + loc.lon;

            }
            else
            {
                throw new WeatherDataServiceException("unsupported location type");
            }
            if (unit == WeatherUnits.Celsius)
            {
                url += "&units=metric";
            }
            else if (unit == WeatherUnits.Fahrenheit)
            {
                url += "&units=imperial";
            }
            else if (unit == WeatherUnits.Kelvin)
            {

            }
            else
            {
                throw new WeatherDataServiceException("unsupported unit type");
            }
            url += "&appid=" + APIKey;
            
            DataContractJsonSerializer parser = new DataContractJsonSerializer(typeof(WeatherListJson));
            
            WeatherListJson json = (WeatherListJson)parser.ReadObject((MakeWebRequest(url)));
            List<WeatherData> data = new List<WeatherData>();
            foreach (WeatherJson j in json.list)
            {
                data.Add(FromJsonToData(j, location, unit));
            }
            return data;
        }

        [DataContract]
        internal class UVJson
        {
            [DataMember]
            internal string time;
            [DataMember]
            internal float data;
        }

        [DataContract]
        internal class WeatherListJson
        {
            [DataMember]
            internal List<WeatherJson> list;
        }

        [DataContract]
        internal class WeatherJson
        {
            [DataMember]
            internal long dt;
            [DataMember]
            internal MainData main;
            [DataMember]
            internal WindData wind;
            [DataMember]
            internal CloudData clouds;
            [DataMember]
            internal List<NameData> weather;
        }

        [DataContract]
        internal class MainData
        {
            [DataMember]
            internal float temp;
            [DataMember]
            internal int humidity;
        }

        [DataContract]
        internal class WindData
        {
            [DataMember]
            internal float speed;
        }

        [DataContract]
        internal class CloudData
        {
            [DataMember]
            internal int all;
        }

        [DataContract]
        internal class NameData
        {
            [DataMember]
            internal string main;
            [DataMember]
            internal string description;
        }
    }
}
