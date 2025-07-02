using CloudinaryDotNet.Actions;
using FluentValidation;
using Footbook.Core.DTOs.Requests.User;
using Footbook.Core.DTOs.Responses.User;
using Footbook.Data.Repositories.Interfaces;
using Footbook.Infrastructure.ExternalServices.Cloudinary;
using Footbook.Infrastructure.Helpers;
using Footbook.Infrastructure.Services.Interfaces;

namespace Footbook.Infrastructure.Services.Implementations;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ICloudinaryService _cloudinaryService;
    private readonly IValidator<UpdateUserRequest> _updateUserValidator;

    public UserService(
        IUserRepository userRepository,
        ICloudinaryService cloudinaryService,
        IValidator<UpdateUserRequest> updateUserValidator)
    {
        _userRepository = userRepository;
        _cloudinaryService = cloudinaryService;
        _updateUserValidator = updateUserValidator;
    }
    
    public async Task<IEnumerable<UserResponse>> GetAllAsync()
    {
        var list = await _userRepository.GetAllAsync();
        return list.Select(u => u.MapToUserResponse());
    }
    
    public async Task<UserResponse> GetByIdAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        
        if (user is null)
        {
            throw new KeyNotFoundException("User not found.");
        }
        
        return user.MapToUserResponse();
    }
    
    public async Task<UserResponse> UpdateAsync(Guid id, UpdateUserRequest request)
    {
        await _updateUserValidator.ValidateAndThrowAsync(request);
        
        var user = request.MapToUser(id);
        
        var image = request.ProfilePicture;
        if (image is not null)
        {
            var imageUrl = user.ProfilePictureUrl;
            if (!string.IsNullOrEmpty(imageUrl))
            {
                await _cloudinaryService.DeleteFileAsync(imageUrl, ResourceType.Image);
            }
            var uploadResult = await _cloudinaryService.UploadImageAsync(image, "users");
            user.ProfilePictureUrl = uploadResult.SecureUrl.ToString();
        }
        
        var updated = await _userRepository.UpdateAsync(user);
        return updated.MapToUserResponse();
    }
    
    public async Task DeleteAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        
        if (user is null)
        {
            throw new KeyNotFoundException("User not found.");
        }
        
        var imageUrl = user.ProfilePictureUrl;
        if (!string.IsNullOrEmpty(imageUrl))
        {
            await _cloudinaryService.DeleteFileAsync(imageUrl, ResourceType.Image);
        }
        
        await _userRepository.DeleteAsync(id);
    }
}