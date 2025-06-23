using Footbook.Core.DTOs.Requests.Slot;
using Footbook.Core.DTOs.Responses.Slot;
using Footbook.Infrastructure.Helpers;

namespace Footbook.Infrastructure.Services.Interfaces;

public interface ISlotService
{
    Task<SlotResponse> CreateAsync(CreateSlotRequest request);
    Task<IEnumerable<SlotResponse>> GetAllAsync();
    Task<SlotResponse> GetByIdAsync(Guid id);
    Task<SlotResponse> UpdateAsync(Guid id, UpdateSlotRequest request);
    Task DeleteAsync(Guid id);
    Task<IEnumerable<SlotResponse>> GetByFieldIdAsync(Guid fieldId);
    Task<PaginatedList<SlotResponse>> SearchAsync(SlotSearchRequest request);
} 