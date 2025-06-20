using Footbook.Data.Models;

namespace Footbook.Data.Repositories.Interfaces;

public interface IBookingRepository
{
    Task<Booking> CreateAsync(Booking booking);
    Task<IEnumerable<Booking>> GetAllAsync();
    Task<Booking?> GetByIdAsync(Guid id);
    Task<IEnumerable<Booking>> GetByUserIdAsync(Guid userId);
    Task<IEnumerable<Booking>> GetBySlotIdAsync(Guid slotId);
    Task<Booking> UpdateAsync(Booking booking);
    Task DeleteAsync(Guid id);
} 