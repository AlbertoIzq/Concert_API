using Concert.UI.Models.DTO;
using Microsoft.AspNetCore.Mvc;

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
                // Get all regions from web API
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
    }
}
