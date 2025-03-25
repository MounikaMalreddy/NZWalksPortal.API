using NZWalksPortal.API.Data;
using NZWalksPortal.API.Models.Domain;
using NZWalksPortal.API.Repositories.Interface;

namespace NZWalksPortal.API.Repositories.Implementation
{
    public class ImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ApplicationDbContext dbContext;

        public ImageRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor,
            ApplicationDbContext dbContext)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.dbContext = dbContext;
        }

        public async Task<Image?> UploadImageAsync(Image image)
        {
            //1. Upload the Image to API/Images folder
            var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images",
                $"{image.FileName}{image.FileExtension}");
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            //2. update the databse
            var httpRequest = httpContextAccessor.HttpContext.Request;
            var urlPath = $"{httpRequest.Scheme}://{httpRequest.Host}{httpRequest.PathBase}/Images/" +
                $"{image.FileName}{image.FileExtension}";
            image.FilePath = urlPath;
            await dbContext.Image.AddAsync(image);
            await dbContext.SaveChangesAsync();
            return image;
        }
    }
}
