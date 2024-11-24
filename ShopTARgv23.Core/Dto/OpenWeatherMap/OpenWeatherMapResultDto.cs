namespace ShopTARgv23.Core.Dto.OpenWeatherMap
{
    public class OpenWeatherMapResultDto
    {
        public string City { get; set; }
        public string CountryCode { get; set; }
        public double Temperature { get; set; }
        public double FeelsLike { get; set; }
        public double MinTemperature { get; set; }
        public double MaxTemperature { get; set; } 
        public string WeatherMain { get; set; }
        public string WeatherDescription { get; set; }
        public double WindSpeed { get; set; }
        public int WindDirection { get; set; } 
        public int Humidity { get; set; }
        public int Pressure { get; set; }
        public int Cloudiness { get; set; }
        public string Sunrise { get; set; }
        public string Sunset { get; set; }
    }
}
