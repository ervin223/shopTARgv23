using ShopTARgv23.Core.Dto.CocktailDto;

namespace ShopTARgv23.Core.ServiceInterface
{
    public interface ICoctailServices
    {
        Task<CoctailResultDto> GetDrink(CoctailResultDto dto);
    }
}
