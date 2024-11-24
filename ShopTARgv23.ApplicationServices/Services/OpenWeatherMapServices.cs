using Nancy.Json;
using ShopTARgv23.Core.ServiceInterface;
using ShopTARgv23.Core.Dto.OpenWeatherMap;
using System.Net;

namespace ShopTARgv23.ApplicationServices.Services
{
    public class OpenWeatherMapServices : IOpenWeatherMap
    {
        public async Task<OpenWeatherMapResultDto> OpenWeatherResult(OpenWeatherMapResultDto dto)
        {
            string apiKey = "588041671253eb42b736a4216772373b"; 
            string url = $"https://api.openweathermap.org/data/2.5/weather?q={dto.City},{dto.CountryCode}&units=metric&appid={apiKey}";

            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(url);

                OpenWeatherMapRoot weatherRoot = new JavaScriptSerializer().Deserialize<OpenWeatherMapRoot>(json);

                dto.City = weatherRoot.Name;
                dto.CountryCode = weatherRoot.Sys.Country;
                dto.Temperature = weatherRoot.Main.Temp;
                dto.FeelsLike = weatherRoot.Main.Feels_Like;
                dto.MinTemperature = weatherRoot.Main.Temp_Min;
                dto.MaxTemperature = weatherRoot.Main.Temp_Max;
                dto.WeatherMain = weatherRoot.Weather[0].Main;
                dto.WeatherDescription = weatherRoot.Weather[0].Description;
                dto.WindSpeed = weatherRoot.Wind.Speed;
                dto.WindDirection = weatherRoot.Wind.Deg;
                dto.Humidity = weatherRoot.Main.Humidity;
                dto.Pressure = weatherRoot.Main.Pressure;
                dto.Cloudiness = weatherRoot.Clouds.All;

                dto.Sunrise = DateTimeOffset.FromUnixTimeSeconds(weatherRoot.Sys.Sunrise).ToLocalTime().ToString("HH:mm");
                dto.Sunset = DateTimeOffset.FromUnixTimeSeconds(weatherRoot.Sys.Sunset).ToLocalTime().ToString("HH:mm");

            }

            return dto;
        }
    }
}
