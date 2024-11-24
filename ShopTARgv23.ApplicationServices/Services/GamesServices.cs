using ShopTARgv23.Core.Dto.Games;
using Nancy.Json;
using ShopTARgv23.Core.ServiceInterface;
using System.Net;

namespace ShopTARgv23.ApplicationServices.Services
{
    public class GamesServices : IGamesServices
    {
        public async Task<GamesResultDto> GamesResult(GamesResultDto dto)
        {
            string url = $"http://www.freetogame.com/api/games";

            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(url);

                List<GamesRootDto> gamesRoot = new JavaScriptSerializer()
                    .Deserialize<List<GamesRootDto>>(json);

                if (dto.GameDto == null)
                {
                    dto.GameDto = new List<GameDto>();
                }

                foreach (var root in gamesRoot)
                {
                    dto.GameDto.Add(new GameDto
                    {
                        id = root.id,
                        title = root.title,
                        thumbnail = root.thumbnail,
                        short_description = root.short_description,
                        game_url = root.game_url,
                        genre = root.genre,
                        platform = root.platform,
                        publisher = root.publisher,
                        developer = root.developer,
                        release_date = root.release_date,
                        freetogame_profile_url = root.freetogame_profile_url
                    });
                }
            }

            return dto;
        }
    }
}
