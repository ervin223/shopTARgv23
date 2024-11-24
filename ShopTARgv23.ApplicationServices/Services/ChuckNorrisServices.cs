using ShopTARgv23.Core.Dto.ChuckNorris;
using ShopTARgv23.Core.ServiceInterface;
using System.Net;
using Nancy.Json;
using System.Threading.Tasks;

namespace ShopTARgv23.ApplicationServices.Services
{
    public class ChuckNorrisServices : IChuckNorrisServices
    {
        public async Task<ChuckNorrisRootDto> GetJokeByCategoryAsync(string category)
        {
            string url = $"https://api.chucknorris.io/jokes/random?category={category}";

            using (WebClient client = new WebClient())
            {
                string json = await client.DownloadStringTaskAsync(url);

                ChuckNorrisRootDto result = new JavaScriptSerializer().Deserialize<ChuckNorrisRootDto>(json);

                return result;
            }
        }
    }
}
