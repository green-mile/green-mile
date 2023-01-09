using Microsoft.AspNetCore.Identity;

using Web.Models;
using Web.Utils;

namespace Web.Services
{
    public class ImageService : IImageService
    {
       public static readonly long UploadSize = 10 * 1024 * 1024;
        public static readonly string destinationFolder = "uploads";
        private readonly IWebHostEnvironment _env;
        private readonly UserManager<User> _userManager;
       
        public ImageService(IWebHostEnvironment env, UserManager<User> userManager)
        {
            _env = env;
            _userManager = userManager;
        }

       async Task<Result<string>> IImageService.RetrieveImage(User user)
        {
            if(user is null)
            {
                return Result<string>.Failure("User cannot be null!");
            }
            return Result<string>.Success("Image successfullly retrieved!", string.IsNullOrEmpty(user.ImageURL) ? "https://upload.wikimedia.org/wikipedia/commons/thumb/2/2c/Default_pfp.svg/2048px-Default_pfp.svg.png" : user.ImageURL);
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
            await _userManager.UpdateAsync(user);
                return Result<string>.Success("Upload successful!", imagePath);
            
        }
    }
}
