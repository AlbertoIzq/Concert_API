using Concert.UI.Models.DTO;
using Concert.UI.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace Concert.UI.Controllers
{
    public class ArtistsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ArtistsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            List<ArtistDto> response = new List<ArtistDto>();

            try
            {
                // Get all artists from web API
                var client = _httpClientFactory.CreateClient();
                var httpResponseMessage = await client.GetAsync("https://localhost:7033/api/artists");

                httpResponseMessage.EnsureSuccessStatusCode();

                response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<ArtistDto>>()); 
            }
            catch (Exception)
            {
                // Log the exception
                throw;
            }

            return View(response);
        }

        public IActionResult Add()
        {
            return View(); 
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddArtistVM addArtistVM)
        {
            var client = _httpClientFactory.CreateClient();
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7033/api/artists"),
                Content = new StringContent(JsonSerializer.Serialize(addArtistVM), Encoding.UTF8, "application/json")
            };
            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();
            
            var response = await httpResponseMessage.Content.ReadFromJsonAsync<ArtistDto>();
            if (response is not null)
            {
                return RedirectToAction("Index", "Artists");
            }

            return View(); 
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetFromJsonAsync<ArtistDto>($"https://localhost:7033/api/artists/{id.ToString()}");

            if (response is not null)
            {
                return View(response);
            }

            return View(null);
        }
    }
}
