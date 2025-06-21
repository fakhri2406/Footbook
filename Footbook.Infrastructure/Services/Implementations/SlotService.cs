using FluentValidation;
using Footbook.Core.DTOs.Requests.Slot;
using Footbook.Core.DTOs.Responses.Slot;
using Footbook.Data.Repositories.Interfaces;
using Footbook.Infrastructure.Helpers;
using Footbook.Infrastructure.Services.Interfaces;

namespace Footbook.Infrastructure.Services.Implementations;

public class SlotService : ISlotService
{
    private readonly ISlotRepository _slotRepository;
    private readonly IValidator<CreateSlotRequest> _createSlotValidator;
    private readonly IValidator<UpdateSlotRequest> _updateSlotValidator;
    
    public SlotService(
        ISlotRepository slotRepository,
        IValidator<CreateSlotRequest> createSlotValidator,
        IValidator<UpdateSlotRequest> updateSlotValidator)
    {
        _slotRepository = slotRepository;
        _createSlotValidator = createSlotValidator;
        _updateSlotValidator = updateSlotValidator;
    }
    
    public async Task<CreateSlotResponse> CreateAsync(CreateSlotRequest request)
    {
        await _createSlotValidator.ValidateAndThrowAsync(request);
        
        var slot = request.MapToSlot();
        var created = await _slotRepository.CreateAsync(slot);
        return created.MapToCreateSlotResponse();
    }
    
    public async Task<IEnumerable<CreateSlotResponse>> GetAllAsync()
    {
        var slots = await _slotRepository.GetAllAsync();
        return slots.Select(s => s.MapToCreateSlotResponse());
    }
    
    public async Task<CreateSlotResponse> GetByIdAsync(Guid id)
    {
        var slot = await _slotRepository.GetByIdAsync(id);
        
        if (slot is null)
        {
            throw new KeyNotFoundException("Slot not found.");
        }

        return slot.MapToCreateSlotResponse();
    }
    
    public async Task<IEnumerable<CreateSlotResponse>> GetByFieldIdAsync(Guid fieldId)
    {
        var slots = await _slotRepository.GetByFieldIdAsync(fieldId);
        return slots.Select(s => s.MapToCreateSlotResponse());
    }
    
    public async Task<UpdateSlotResponse> UpdateAsync(Guid id, UpdateSlotRequest request)
    {
        await _updateSlotValidator.ValidateAndThrowAsync(request);
        
        var slot = request.MapToSlot(id);
        var updated = await _slotRepository.UpdateAsync(slot);
        return updated.MapToUpdateSlotResponse();
    }
    
    public async Task DeleteAsync(Guid id)
    {
        await _slotRepository.DeleteAsync(id);
    }
} 