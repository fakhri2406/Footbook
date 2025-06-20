using Footbook.Core.DTOs.Requests.Slot;
using Footbook.Core.DTOs.Responses.Slot;
using Footbook.Data.Repositories.Interfaces;
using Footbook.Infrastructure.Helpers;
using Footbook.Infrastructure.Services.Interfaces;

namespace Footbook.Infrastructure.Services.Implementations;

public class SlotService : ISlotService
{
    private readonly ISlotRepository _slotRepository;
    
    public SlotService(ISlotRepository slotRepository)
        => _slotRepository = slotRepository;
    
    public async Task<CreateSlotResponse> CreateAsync(CreateSlotRequest request)
    {
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
        var slot = request.MapToSlot(id);
        var updated = await _slotRepository.UpdateAsync(slot);
        return updated.MapToUpdateSlotResponse();
    }
    
    public async Task DeleteAsync(Guid id)
    {
        await _slotRepository.DeleteAsync(id);
    }
} 