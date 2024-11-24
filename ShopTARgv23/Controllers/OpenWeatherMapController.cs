using Microsoft.AspNetCore.Mvc;
using ShopTARgv23.Core.Dto.OpenWeatherMap;
using ShopTARgv23.Core.ServiceInterface;
using ShopTARgv23.Models.OpenWeatherMap;

namespace ShopTARgv23.Controllers
{
    public class OpenWeatherMapController : Controller
    {
        private readonly IOpenWeatherMap _services;
        public OpenWeatherMapController (IOpenWeatherMap services)
        {
            _services = services;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetCityWeather(OpenWeatherMapViewModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("CityWeather", "OpenWeatherMap", 
                    new { cityName = model.City, countryCode = model.CountryCode });
            }

            return View("Index", model);
        }

        [HttpGet] 
        public IActionResult CityWeather(string cityName, string countryCode)
        {
            OpenWeatherMapResultDto dto = new()
            {
                City = cityName,
                CountryCode = countryCode
            };

            _services.OpenWeatherResult(dto);
            
            WeatherDetailsViewModel vm = new()
            {
                City = dto.City,
                CountryCode = dto.CountryCode,
                Temperature = dto.Temperature,
                FeelsLike = dto.FeelsLike,
                MinTemperature = dto.MinTemperature,
                MaxTemperature = dto.MaxTemperature,
                WeatherMain = dto.WeatherMain,
                WeatherDescription = dto.WeatherDescription,
                WindSpeed = dto.WindSpeed,
                WindDirection = dto.WindDirection,
                Humidity = dto.Humidity,
                Pressure = dto.Pressure,
                Cloudiness = dto.Cloudiness,
                Sunrise = dto.Sunrise,
                Sunset = dto.Sunset
            };

            return View(vm);
        }
    }
}
