using System.ComponentModel.DataAnnotations;

namespace ShopTARgv23.Models.OpenWeatherMap
{
    public class OpenWeatherMapViewModel
    {
        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Country code is required.")]
        public string CountryCode { get; set; }
    }
}
