using Web.Models;
using Web.Utils;

namespace Web.Services
{
    public interface IImageService
    {
       

        public Task<Result<string>> StoreImage(IFormFile image, User user);
        public Task<Result<string>> RetrieveImage(User user);

    }
}
