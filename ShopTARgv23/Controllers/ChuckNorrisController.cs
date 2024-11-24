using Microsoft.AspNetCore.Mvc;
using ShopTARgv23.Core.Dto.ChuckNorris;
using ShopTARgv23.Core.ServiceInterface;
using ShopTARgv23.Models.ChuckNorris;

namespace ShopTARgv23.Controllers
{
    public class ChuckNorrisController : Controller
    {
        private readonly IChuckNorrisServices _services;
        public ChuckNorrisController (IChuckNorrisServices services)
        {
            _services = services;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetRandomJokeByCategory(string category)
        {
            var joke = await _services.GetJokeByCategoryAsync(category);

            var model = new JokeViewModel
            {
                Categories = joke.Categories,
                CreatedAt = joke.CreatedAt,
                IconUrl = joke.IconUrl,
                Id = joke.Id,
                UpdatedAt = joke.UpdatedAt,
                Url = joke.Url,
                Value = joke.Value
            };

            return View("Joke", model);
        }
    }
}