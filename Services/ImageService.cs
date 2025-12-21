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
                return Task.CompletedTask;

            // Convert web path (with /) to OS-specific file system path
            var normalizedPath = imagePath.Replace('/', Path.DirectorySeparatorChar);
            var fullPath = Path.Combine(_environment.WebRootPath, normalizedPath);

            // Delete the file if it exists
            if (File.Exists(fullPath))
                File.Delete(fullPath);

            return Task.CompletedTask;
        }

        public Task<(Stream? Stream, string? ContentType)?> GetImageAsync(string? imagePath)
        {
            if (string.IsNullOrEmpty(imagePath))
                return Task.FromResult<(Stream?, string?)?>(null);

            // Convert web path (with /) to OS-specific file system path
            var normalizedPath = imagePath.Replace('/', Path.DirectorySeparatorChar);
            var fullPath = Path.Combine(_environment.WebRootPath, normalizedPath);
            if (!File.Exists(fullPath))
                return Task.FromResult<(Stream?, string?)?>(null);

            // Open the file as a read-only stream
            var stream = File.OpenRead(fullPath);
            var extension = Path.GetExtension(fullPath).ToLower();

            var contentType = extension switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",  // Standard MIME for JPEG
                ".png" => "image/png",             // Standard MIME for PNG
                ".gif" => "image/gif",             // Standard MIME for GIF
                _ => "application/octet-stream"     // Default for unknown
            };

            // Return the stream and content type as a tuple
            return Task.FromResult<(Stream?, string?)?>((stream, contentType));
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
            try
            {
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream); // Copy uploaded file to disk
                }
            }
            catch (IOException ex)
            {
                throw new InvalidOperationException("Failed to save image file.", ex);
            }
            // Return relative path for DB (always use forward slashes for web URLs)
            return $"images/{uniqueFileName}";
        }
    }
}



// //  CHANGE 2: Added path validation for security
// // Make sure the path is actually inside wwwroot (prevent path traversal attacks)
// var rootPath = Path.GetFullPath(_environment.WebRootPath);
// var resolvedPath = Path.GetFullPath(fullPath);

// if (!resolvedPath.StartsWith(rootPath, StringComparison.OrdinalIgnoreCase))
// {
//     // Someone tried to delete a file outside wwwroot - security issue!
//     throw new InvalidOperationException("Invalid image path.");
// }