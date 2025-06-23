using FluentValidation;
using Footbook.Core.DTOs.Requests.Slot;
using Footbook.Core.DTOs.Responses.Slot;
using Footbook.Data.Repositories.Interfaces;
using Footbook.Infrastructure.Helpers;
using Footbook.Infrastructure.Services.Interfaces;
using System.Linq;

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
    
    public async Task<SlotResponse> CreateAsync(CreateSlotRequest request)
    {
        await _createSlotValidator.ValidateAndThrowAsync(request);
        
        var slot = request.MapToSlot();
        var created = await _slotRepository.CreateAsync(slot);
        return created.MapToSlotResponse();
    }
    
    public async Task<IEnumerable<SlotResponse>> GetAllAsync()
    {
        var slots = await _slotRepository.GetAllAsync();
        return slots.Select(s => s.MapToSlotResponse());
    }
    
    public async Task<SlotResponse> GetByIdAsync(Guid id)
    {
        var slot = await _slotRepository.GetByIdAsync(id);
        
        if (slot is null)
        {
            throw new KeyNotFoundException("Slot not found.");
        }

        return slot.MapToSlotResponse();
    }
    
    public async Task<IEnumerable<SlotResponse>> GetByFieldIdAsync(Guid fieldId)
    {
        var slots = await _slotRepository.GetByFieldIdAsync(fieldId);
        return slots.Select(s => s.MapToSlotResponse());
    }
    
    public async Task<SlotResponse> UpdateAsync(Guid id, UpdateSlotRequest request)
    {
        await _updateSlotValidator.ValidateAndThrowAsync(request);
        
        var slot = request.MapToSlot(id);
        var updated = await _slotRepository.UpdateAsync(slot);
        return updated.MapToSlotResponse();
    }
    
    public async Task DeleteAsync(Guid id)
    {
        await _slotRepository.DeleteAsync(id);
    }
    
    public async Task<PaginatedList<SlotResponse>> SearchAsync(SlotSearchRequest request)
    {
        var (slots, totalCount) = await _slotRepository.SearchAsync(
            request.StadiumName,
            request.Date,
            request.Region,
            request.OnlyOpen,
            request.Page,
            request.PageSize);
        
        var items = slots.Select(s => s.MapToSlotResponse()).ToList();
        return new PaginatedList<SlotResponse>(items, totalCount, request.Page, request.PageSize);
    }
} 