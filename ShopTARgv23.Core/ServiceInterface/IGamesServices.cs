using ShopTARgv23.Core.Dto.Games;

namespace ShopTARgv23.Core.ServiceInterface
{
    public interface IGamesServices
    {
        Task<GamesResultDto> GamesResult(GamesResultDto dto);
    }
}
