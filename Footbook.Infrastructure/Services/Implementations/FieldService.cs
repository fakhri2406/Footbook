using FluentValidation;
using Footbook.Core.DTOs.Requests.Field;
using Footbook.Core.DTOs.Responses.Field;
using Footbook.Data.Repositories.Interfaces;
using Footbook.Infrastructure.Helpers;
using Footbook.Infrastructure.Services.Interfaces;

namespace Footbook.Infrastructure.Services.Implementations;

public class FieldService : IFieldService
{
    private readonly IFieldRepository _fieldRepository;
    private readonly IValidator<CreateFieldRequest> _createFieldValidator;
    private readonly IValidator<UpdateFieldRequest> _updateFieldValidator;
    
    public FieldService(
        IFieldRepository fieldRepository,
        IValidator<CreateFieldRequest> createFieldValidator,
        IValidator<UpdateFieldRequest> updateFieldValidator)
    {
        _fieldRepository = fieldRepository;
        _createFieldValidator = createFieldValidator;
        _updateFieldValidator = updateFieldValidator;
    }
    
    public async Task<CreateFieldResponse> CreateAsync(CreateFieldRequest request)
    {
        await _createFieldValidator.ValidateAndThrowAsync(request);
        
        var field = request.MapToField();
        var created = await _fieldRepository.CreateAsync(field);
        return created.MapToCreateFieldResponse();
    }
    
    public async Task<IEnumerable<CreateFieldResponse>> GetAllAsync()
    {
        var list = await _fieldRepository.GetAllAsync();
        return list.Select(f => f.MapToCreateFieldResponse());
    }
    
    public async Task<CreateFieldResponse> GetByIdAsync(Guid id)
    {
        var field = await _fieldRepository.GetByIdAsync(id);
        
        if (field is null)
        {
            throw new KeyNotFoundException("Field not found.");
        }

        return field.MapToCreateFieldResponse();
    }
    
    public async Task<UpdateFieldResponse> UpdateAsync(Guid id, UpdateFieldRequest request)
    {
        await _updateFieldValidator.ValidateAndThrowAsync(request);
        
        var field = request.MapToField(id);
        var updated = await _fieldRepository.UpdateAsync(field);
        return updated.MapToUpdateFieldResponse();
    }
    
    public async Task DeleteAsync(Guid id)
    {
        await _fieldRepository.DeleteAsync(id);
    }
} 