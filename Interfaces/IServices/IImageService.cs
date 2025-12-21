using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryV2.Interfaces.IServices
{
    public interface IImageService
    {
        Task<string?> UploadImageAsync(IFormFile file);
        Task DeleteImageAsync(string? imagePath);
        Task<(Stream? Stream, string? ContentType)?> GetImageAsync(string? imagePath);
    }
}