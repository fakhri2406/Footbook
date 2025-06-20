using Footbook.Core.DTOs.Requests.Slot;
using Footbook.Core.DTOs.Responses.Slot;

namespace Footbook.Infrastructure.Services.Interfaces;

public interface ISlotService
{
    Task<CreateSlotResponse> CreateAsync(CreateSlotRequest request);
    Task<IEnumerable<CreateSlotResponse>> GetAllAsync();
    Task<CreateSlotResponse> GetByIdAsync(Guid id);
    Task<UpdateSlotResponse> UpdateAsync(Guid id, UpdateSlotRequest request);
    Task DeleteAsync(Guid id);
    Task<IEnumerable<CreateSlotResponse>> GetByFieldIdAsync(Guid fieldId);
} 