using Nancy.Json;
using ShopTARgv23.Core.Dto.WeatherDtos;
using ShopTARgv23.Core.ServiceInterface;
using System.Net;

namespace ShopTARgv23.ApplicationServices.Services
{
    public class WeatherForecastServices : IWeatherForecastServices
    {
        public async Task<AccuLocationWeatherResultDto> AccuWeatherResult(AccuLocationWeatherResultDto dto)
        {
            string accuApiKey = "ci2IwqXfsFAJYGbeT0Rz1IVO97KQo4sy";
            string url = $"http://dataservice.accuweather.com/locations/v1/cities/search?apikey={accuApiKey}&q={dto.CityName}";

            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(url);

                List<AccuLocationRootDto> accuResult = new JavaScriptSerializer()
                    .Deserialize<List<AccuLocationRootDto>>(json);

                var cityInfo = accuResult[0];
                dto.CityName = cityInfo.LocalizedName;
                dto.CityCode = cityInfo.Key;
                dto.Rank = cityInfo.Rank;
            }

            string urlWeather = $"http://dataservice.accuweather.com/forecasts/v1/daily/1day/{dto.CityCode}?apikey={accuApiKey}&metric=true";

            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(urlWeather);
                AccuWeatherRootDto weatherRootDto = new JavaScriptSerializer()
                    .Deserialize<AccuWeatherRootDto>(json);

                var headline = weatherRootDto.Headline;

                dto.EffectiveDate = headline.EffectiveDate;
                dto.EffectiveEpochDate = headline.EffectiveEpochDate;
                dto.Severity = headline.Severity;
                dto.Text = headline.Text;
                dto.Category = headline.Category;
                dto.EndDate = headline.EndDate;
                dto.EndEpochDate = headline.EndEpochDate;
                dto.Link = headline.Link;
                dto.MobileLink = headline.MobileLink;

                var forecast = weatherRootDto.DailyForecasts[0];

                dto.DailyForecastsDate = forecast.Date;
                dto.DailyForecastsEpochDate = forecast.EpochDate;

                dto.TempMinValue = forecast.Temperature.Minimum.Value;
                dto.TempMinUnit = forecast.Temperature.Minimum.Unit;
                dto.TempMinUnitType = forecast.Temperature.Minimum.UnitType;
                dto.TempMaxValue = forecast.Temperature.Maximum.Value;
                dto.TempMaxUnit = forecast.Temperature.Maximum.Unit;
                dto.TempMaxUnitType = forecast.Temperature.Maximum.UnitType;

                dto.DayIcon = forecast.Day.Icon;
                dto.DayIconPhrase = forecast.Day.IconPhrase;
                dto.DayHasPrecipitation = forecast.Day.HasPrecipitation;
                dto.DayPrecipitationType = forecast.Day.PrecipitationType;
                dto.DayPrecipitationIntensity = forecast.Day.PrecipitationIntensity;

                dto.NightIcon = forecast.Night.Icon;
                dto.NightIconPhrase = forecast.Night.IconPhrase;
                dto.NightHasPrecipitation = forecast.Night.HasPrecipitation;
                dto.DayPrecipitationType = forecast.Night.PrecipitationType;
                dto.DayPrecipitationIntensity = forecast.Night.PrecipitationIntensity;

                dto.MobileLink = forecast.MobileLink;
                dto.Link = forecast.Link;
            }

            return dto;
        }
    }
}
