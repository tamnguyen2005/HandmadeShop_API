using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using HandmadeShop.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace HandmadeShop.Infrastructure.Provider
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary _cloudinary;

        public PhotoService(IConfiguration configuration)
        {
            var acc = new Account(configuration["Cloudinary:CloudName"],
                                  configuration["Cloudinary:APIKey"],
                                  configuration["Cloudinary:SecretKey"]);
            _cloudinary = new Cloudinary(acc);
        }

        public async Task<string> UploadAsync(IFormFile avatarURL)
        {
            var imageResult = new ImageUploadResult();
            if (avatarURL.Length > 0)
            {
                using var stream = avatarURL.OpenReadStream();
                var uploadParam = new ImageUploadParams()
                {
                    File = new FileDescription(avatarURL.FileName, stream),
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")
                };
                imageResult = await _cloudinary.UploadAsync(uploadParam);
            }
            return imageResult.SecureUrl.ToString();
        }
    }
}