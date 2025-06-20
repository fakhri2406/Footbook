using Footbook.Core.DTOs.Requests.Booking;
using Footbook.Core.DTOs.Responses.Booking;

namespace Footbook.Infrastructure.Services.Interfaces;

public interface IBookingService
{
    Task<CreateBookingResponse> CreateAsync(CreateBookingRequest request);
    Task<IEnumerable<CreateBookingResponse>> GetAllAsync();
    Task<CreateBookingResponse> GetByIdAsync(Guid id);
    Task<UpdateBookingResponse> UpdateAsync(Guid id, UpdateBookingRequest request);
    Task DeleteAsync(Guid id);
    Task<IEnumerable<CreateBookingResponse>> GetByUserIdAsync(Guid userId);
    Task<IEnumerable<CreateBookingResponse>> GetBySlotIdAsync(Guid slotId);
} 