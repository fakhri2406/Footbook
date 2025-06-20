using Footbook.Data.Models;

namespace Footbook.Data.Repositories.Interfaces;

public interface ISlotRepository
{
    Task<Slot> CreateAsync(Slot slot);
    Task<IEnumerable<Slot>> GetAllAsync();
    Task<Slot?> GetByIdAsync(Guid id);
    Task<IEnumerable<Slot>> GetByFieldIdAsync(Guid fieldId);
    Task<Slot> UpdateAsync(Slot slot);
    Task DeleteAsync(Guid id);
} 