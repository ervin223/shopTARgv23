using ShopTARgv23.Core.Dto.OpenWeatherMap;

namespace ShopTARgv23.Core.ServiceInterface
{
    public interface IOpenWeatherMap
    {
        Task<OpenWeatherMapResultDto> OpenWeatherResult(OpenWeatherMapResultDto dto);
    }
}
