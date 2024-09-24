using Concert.API.Models.Domain;
using Concert.Utility;

namespace Concert.API.Repositories
{
    public interface IImageRepository
    {
        Task<Image> UploadAsync(Image image);
    }
}
