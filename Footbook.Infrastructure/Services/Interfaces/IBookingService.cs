using Footbook.Core.DTOs.Requests.Booking;
using Footbook.Core.DTOs.Responses.Booking;

namespace Footbook.Infrastructure.Services.Interfaces;

public interface IBookingService
{
    Task<BookingResponse> CreateAsync(CreateBookingRequest request);
    Task<IEnumerable<BookingResponse>> GetAllAsync();
    Task<BookingResponse> GetByIdAsync(Guid id);
    Task<BookingResponse> UpdateAsync(Guid id, UpdateBookingRequest request);
    Task DeleteAsync(Guid id);
    Task<IEnumerable<BookingResponse>> GetByUserIdAsync(Guid userId);
    Task<IEnumerable<BookingResponse>> GetBySlotIdAsync(Guid slotId);
} 