using CloudinaryDotNet.Actions;
using FluentValidation;
using Footbook.Core.DTOs.Requests.Stadium;
using Footbook.Core.DTOs.Responses.Stadium;
using Footbook.Data.Repositories.Interfaces;
using Footbook.Infrastructure.ExternalServices.Cloudinary;
using Footbook.Infrastructure.Helpers;
using Footbook.Infrastructure.Services.Interfaces;

namespace Footbook.Infrastructure.Services.Implementations;

public class StadiumService : IStadiumService
{
    private readonly IStadiumRepository _stadiumRepository;
    private readonly ICloudinaryService _cloudinaryService;
    private readonly IValidator<CreateStadiumRequest> _createStadiumValidator;
    private readonly IValidator<UpdateStadiumRequest> _updateStadiumValidator;
    
    public StadiumService(
        IStadiumRepository stadiumRepository,
        ICloudinaryService cloudinaryService,
        IValidator<CreateStadiumRequest> createStadiumValidator,
        IValidator<UpdateStadiumRequest> updateStadiumValidator)
    {
        _stadiumRepository = stadiumRepository;
        _cloudinaryService = cloudinaryService;
        _createStadiumValidator = createStadiumValidator;
        _updateStadiumValidator = updateStadiumValidator;
    }
    
    public async Task<StadiumResponse> CreateAsync(CreateStadiumRequest request)
    {
        await _createStadiumValidator.ValidateAndThrowAsync(request);
        
        var stadium = request.MapToStadium();
        
        var image = request.Image;
        if (image is not null)
        {
            var uploadResult = await _cloudinaryService.UploadImageAsync(image, "stadiums");
            stadium.ImageUrl = uploadResult.SecureUrl.ToString();
        }
        
        var created = await _stadiumRepository.CreateAsync(stadium);
        return created.MapToStadiumResponse();
    }
    
    public async Task<IEnumerable<StadiumResponse>> GetAllAsync()
    {
        var stadiums = await _stadiumRepository.GetAllAsync();
        return stadiums.Select(s => s.MapToStadiumResponse());
    }
    
    public async Task<StadiumResponse> GetByIdAsync(Guid id)
    {
        var stadium = await _stadiumRepository.GetByIdAsync(id);
        
        if (stadium is null)
        {
            throw new KeyNotFoundException("Stadium not found.");
        }
        
        return stadium.MapToStadiumResponse();
    }

    public async Task<StadiumResponse> UpdateAsync(Guid id, UpdateStadiumRequest request)
    {
        await _updateStadiumValidator.ValidateAndThrowAsync(request);
        
        var stadium = request.MapToStadium(id);
        
        var image = request.Image;
        if (image is not null)
        {
            var imageUrl = stadium.ImageUrl;
            if (!string.IsNullOrEmpty(imageUrl))
            {
                await _cloudinaryService.DeleteFileAsync(imageUrl, ResourceType.Image);
            }
            var uploadResult = await _cloudinaryService.UploadImageAsync(image, "stadiums");
            stadium.ImageUrl = uploadResult.SecureUrl.ToString();
        }
        
        var updated = await _stadiumRepository.UpdateAsync(stadium);
        return updated.MapToStadiumResponse();
    }
    
    public async Task DeleteAsync(Guid id)
    {
        var stadium = await _stadiumRepository.GetByIdAsync(id);
        
        if (stadium is null)
        {
            throw new KeyNotFoundException("Stadium not found.");
        }
        
        var imageUrl = stadium.ImageUrl;
        if (!string.IsNullOrEmpty(imageUrl))
        {
            await _cloudinaryService.DeleteFileAsync(imageUrl, ResourceType.Image);
        }
        
        await _stadiumRepository.DeleteAsync(id);
    }
} 