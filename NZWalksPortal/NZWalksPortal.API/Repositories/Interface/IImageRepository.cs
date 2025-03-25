using NZWalksPortal.API.Models.Domain;

namespace NZWalksPortal.API.Repositories.Interface
{
    public interface IImageRepository
    {
        Task<Image?> UploadImageAsync(Image image);
    }
}
