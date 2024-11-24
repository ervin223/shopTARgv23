using Microsoft.AspNetCore.Mvc;
using ShopTARgv23.Core.Dto.WeatherDtos;
using ShopTARgv23.Core.ServiceInterface;
using ShopTARgv23.Models.AccuWeather;

namespace ShopTARgv23.Controllers
{
    public class AccuWeatherController : Controller
    {
        private readonly IWeatherForecastServices _weatherForecastServices;

        public AccuWeatherController(IWeatherForecastServices weatherForecastServices)
        {
            _weatherForecastServices = weatherForecastServices;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SearchCity(AccuWeatherSearchViewModel model)
        {
            if (ModelState.IsValid) 
            { 
                return RedirectToAction("City", "AccuWeather", new { city = model.CityName });
            }

            return View(model);
        }

        [HttpGet]        
        public IActionResult City(string city)
        {
            AccuLocationWeatherResultDto dto = new();
            dto.CityName = city;

            _weatherForecastServices.AccuWeatherResult(dto);
            AccuWeatherViewModel vm = new() 
            {
                CityName = dto.CityName,
                EffectiveDate = dto.EffectiveDate,
                EffectiveEpochDate = dto.EffectiveEpochDate,
                Severity = dto.Severity,
                Text = dto.Text,
                Category = dto.Category,
                EndDate = dto.EndDate,
                EndEpochDate = dto.EndEpochDate,
                DailyForecastsDate = dto.DailyForecastsDate,
                DailyForecastsEpochDate = dto.DailyForecastsEpochDate,
                TempMinValue = dto.TempMinValue,
                TempMinUnit = dto.TempMinUnit,
                TempMinUnitType = dto.TempMinUnitType,
                TempMaxValue = dto.TempMaxValue,
                TempMaxUnit = dto.TempMaxUnit,
                TempMaxUnitType = dto.TempMaxUnitType,
                DayIcon = dto.DayIcon,
                DayIconPhrase = dto.DayIconPhrase,
                DayHasPrecipitation = dto.DayHasPrecipitation,
                DayPrecipitationType = dto.DayPrecipitationType,
                DayPrecipitationIntensity = dto.DayPrecipitationIntensity,
                NightIcon = dto.NightIcon,
                NightIconPhrase = dto.NightIconPhrase,
                NightHasPrecipitation = dto.NightHasPrecipitation,
                NightPrecipitationType = dto.NightPrecipitationType,
                NightPrecipitationIntensity = dto.NightPrecipitationIntensity,
                MobileLink = dto.MobileLink,
                Link = dto.Link
            };

            return View(vm);
        }
    }
}
