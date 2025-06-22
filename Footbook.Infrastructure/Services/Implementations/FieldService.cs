using CloudinaryDotNet.Actions;
using FluentValidation;
using Footbook.Core.DTOs.Requests.Field;
using Footbook.Core.DTOs.Responses.Field;
using Footbook.Data.Repositories.Interfaces;
using Footbook.Infrastructure.ExternalServices.Cloudinary;
using Footbook.Infrastructure.Helpers;
using Footbook.Infrastructure.Services.Interfaces;

namespace Footbook.Infrastructure.Services.Implementations;

public class FieldService : IFieldService
{
    private readonly IFieldRepository _fieldRepository;
    private readonly ICloudinaryService _cloudinaryService;
    private readonly IValidator<CreateFieldRequest> _createFieldValidator;
    private readonly IValidator<UpdateFieldRequest> _updateFieldValidator;
    
    public FieldService(
        IFieldRepository fieldRepository,
        ICloudinaryService cloudinaryService,
        IValidator<CreateFieldRequest> createFieldValidator,
        IValidator<UpdateFieldRequest> updateFieldValidator)
    {
        _fieldRepository = fieldRepository;
        _cloudinaryService = cloudinaryService;
        _createFieldValidator = createFieldValidator;
        _updateFieldValidator = updateFieldValidator;
    }
    
    public async Task<FieldResponse> CreateAsync(CreateFieldRequest request)
    {
        await _createFieldValidator.ValidateAndThrowAsync(request);
        
        var field = request.MapToField();
        
        var image = request.Image;
        if (image is not null)
        {
            var uploadResult = await _cloudinaryService.UploadImageAsync(image, "fields");
            field.ImageUrl = uploadResult.SecureUrl.ToString();
        }
        
        var created = await _fieldRepository.CreateAsync(field);
        return created.MapToFieldResponse();
    }
    
    public async Task<IEnumerable<FieldResponse>> GetAllAsync()
    {
        var list = await _fieldRepository.GetAllAsync();
        return list.Select(f => f.MapToFieldResponse());
    }
    
    public async Task<FieldResponse> GetByIdAsync(Guid id)
    {
        var field = await _fieldRepository.GetByIdAsync(id);
        
        if (field is null)
        {
            throw new KeyNotFoundException("Field not found.");
        }

        return field.MapToFieldResponse();
    }
    
    public async Task<FieldResponse> UpdateAsync(Guid id, UpdateFieldRequest request)
    {
        await _updateFieldValidator.ValidateAndThrowAsync(request);
        
        var field = request.MapToField(id);
        
        var image = request.Image;
        if (image is not null)
        {
            var imageUrl = field.ImageUrl;
            if (!string.IsNullOrEmpty(imageUrl))
            {
                await _cloudinaryService.DeleteFileAsync(imageUrl, ResourceType.Image);
            }
            var uploadResult = await _cloudinaryService.UploadImageAsync(image, "fields");
            field.ImageUrl = uploadResult.SecureUrl.ToString();
        }
        
        var updated = await _fieldRepository.UpdateAsync(field);
        return updated.MapToFieldResponse();
    }
    
    public async Task DeleteAsync(Guid id)
    {
        var field = await _fieldRepository.GetByIdAsync(id);
        
        if (field is null)
        {
            throw new KeyNotFoundException("Field not found.");
        }
        
        var imageUrl = field.ImageUrl;
        if (!string.IsNullOrEmpty(imageUrl))
        {
            await _cloudinaryService.DeleteFileAsync(imageUrl, ResourceType.Image);
        }
        
        await _fieldRepository.DeleteAsync(id);
    }
} 