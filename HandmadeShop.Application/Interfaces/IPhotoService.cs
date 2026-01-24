using Microsoft.AspNetCore.Http;

namespace HandmadeShop.Application.Interfaces
{
    public interface IPhotoService
    {
        Task<string> UploadAsync(IFormFile avatarURL);
    }
}