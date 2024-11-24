using ShopTARgv23.Core.Dto.ChuckNorris;
using System.Threading.Tasks;

namespace ShopTARgv23.Core.ServiceInterface
{
    public interface IChuckNorrisServices
    {
        Task<ChuckNorrisRootDto> GetJokeByCategoryAsync(string category);
    }
}
