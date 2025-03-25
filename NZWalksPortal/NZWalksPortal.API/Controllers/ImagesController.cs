using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalksPortal.API.Models.Domain;
using NZWalksPortal.API.Models.DTO;
using NZWalksPortal.API.Repositories.Interface;

namespace NZWalksPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        [HttpPost]
        [Route("UploadImage")]
        public async Task<IActionResult> UploadImage(ImageUploadRequestDto request)
        {
            ValidateFileUpload(request);
            if (ModelState.IsValid)
            {
                var imageDomain = new Image
                {
                    File = request.File,
                    FileName = request.FileName,
                    FileDescription = request.FileDescription,
                    FileExtension = Path.GetExtension(request.File.FileName),
                    FileSizeInBytes=request.File.Length
                };
                imageDomain = await imageRepository.UploadImageAsync(imageDomain);
                return Ok(imageDomain);
            }
            return BadRequest(ModelState);
        }
        private void ValidateFileUpload(ImageUploadRequestDto request)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            if (!allowedExtensions.Contains(Path.GetExtension(request.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported File Extension.");
            }
            if (request.File.Length>10485760)
            {
                ModelState.AddModelError("file", "File size should not exceed 10MB, Please Upload Smaller Size File.");
            }
        }
    }
}
