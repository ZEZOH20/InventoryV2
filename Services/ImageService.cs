using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryV2.Interfaces.IServices;

namespace InventoryV2.Services
{
    public class ImageService : IImageService
    {
        readonly IWebHostEnvironment _environment;
        public ImageService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        public Task DeleteImageAsync(string? imagePath)
        {
            if (string.IsNullOrEmpty(imagePath))
                throw new ArgumentException("there isn't image to delete");
            imagePath = imagePath.Replace('\\', Path.DirectorySeparatorChar)
                                 .Replace('/', Path.DirectorySeparatorChar);

            var fullPath = Path.Combine(_environment.WebRootPath, imagePath);
            throw new NotImplementedException();
        }

        public async Task<string?> UploadImageAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return null;

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var extension = Path.GetExtension(file.FileName).ToLower();

            if (!allowedExtensions.Contains(extension))
                throw new InvalidOperationException("Only image files (JPG, PNG, GIF) are allowed.");

            const long maxFileSize = 5 * 1024 * 1024;  // 5MB in bytes
            if (file.Length > maxFileSize)
                throw new InvalidOperationException("Image size must be less than 5MB.");

            var uploadsFolder = Path.Combine(_environment.WebRootPath, "images");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);  // Make folder if needed

            var datePart = DateTime.UtcNow.ToString("yyyy-MM-dd-HHmmss");
            var uniqueFileName = $"{Guid.NewGuid()}-{datePart}{extension}";

            var filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream); // Copy uploaded file to disk
            }

            // Return relative path for DB (e.g., "images/products/guid.jpg")
            return Path.Combine("images", uniqueFileName).Replace('\\', Path.DirectorySeparatorChar)
                                                         .Replace('/', Path.DirectorySeparatorChar);
        }
    }
}