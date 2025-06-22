using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace Footbook.Infrastructure.ExternalServices.Cloudinary;

public interface ICloudinaryService
{
    Task<ImageUploadResult> UploadImageAsync(IFormFile image, string folder);
    Task<VideoUploadResult> UploadVideoAsync(IFormFile video, string folder);
    Task<DeletionResult> DeleteFileAsync(string publicId, ResourceType resourceType);
}