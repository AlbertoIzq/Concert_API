using Concert.API.Data;
using Concert.API.Models.Domain;
using Concert.API.Models.DTO;
using Concert.Utility;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Concert.API.Repositories
{
    public class LocalImageRepository : IImageRepository    
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ConcertDbContext _concertDbContext;

        public LocalImageRepository(IWebHostEnvironment webHostEnvironment,
            IHttpContextAccessor httpContextAccessor,
            ConcertDbContext concertDbContext)
        {
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
            _concertDbContext = concertDbContext;
        }

        public async Task<Image> UploadAsync(Image image)
        {
            var localFilePath = Path.Combine(_webHostEnvironment.ContentRootPath, SD.IMAGES_FOLDER_NAME,
                $"{image.FileName}{image.FileExtension}");

            // Upload image to local path
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            // e.g. https://localhost:1234/images/imageName.jpg
            var urlFilePath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://" + // https://
                $"{_httpContextAccessor.HttpContext.Request.Host}" + // localhost:1234
                $"{_httpContextAccessor.HttpContext.Request.PathBase}/" // /
                + $"{SD.IMAGES_FOLDER_NAME}/{image.FileName}{image.FileExtension}"; // images/imageName.jpg

            image.FilePath = urlFilePath;

            // Add the image to the Images table
            await _concertDbContext.Images.AddAsync(image);
            await _concertDbContext.SaveChangesAsync();

            return image;
        }
    }
}
