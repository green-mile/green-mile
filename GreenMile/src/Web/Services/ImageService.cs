using Web.Models;
using Web.Utils;

namespace Web.Services
{
    public class ImageService : IImageService
    {
       public static readonly long UploadSize = 10 * 1024 * 1024;
        public static readonly string destinationFolder = "uploads";
        private readonly IWebHostEnvironment _env;
       
        public ImageService(IWebHostEnvironment env)
        {
            _env = env;
        }

       async Task<Result<string>> IImageService.RetrieveImage(User user)
        {

            throw new NotImplementedException();
        }

         async Task<Result<string>> IImageService.StoreImage(IFormFile image, User user)
        {
            if(image == null)
            {
                return Result<string>.Failure("Image is null!");
            }
            else if (image.Length > UploadSize)
            {
                return Result<string>.Failure("Upload size is too big!");
            }
          
                string imageFile = Guid.NewGuid() + Path.GetExtension(image.FileName);
                string imagePath = Path.Combine(_env.ContentRootPath, "wwwroot", destinationFolder, imageFile);
                using var fileStream = new FileStream(imagePath, FileMode.Create);
                await image.CopyToAsync(fileStream);
                user.ImageURL = $"/{destinationFolder}/{imageFile}";
                return Result<string>.Success("Upload successful!", imagePath);
            
        }
    }
}
