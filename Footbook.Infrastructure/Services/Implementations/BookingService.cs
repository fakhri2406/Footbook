using Footbook.Core.DTOs.Requests.Booking;
using Footbook.Core.DTOs.Responses.Booking;
using Footbook.Data.Repositories.Interfaces;
using Footbook.Infrastructure.Helpers;
using Footbook.Infrastructure.Services.Interfaces;

namespace Footbook.Infrastructure.Services.Implementations;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly ISlotRepository _slotRepository;
    
    public BookingService(
        IBookingRepository bookingRepository,
        ISlotRepository slotRepository)
    {
        _bookingRepository = bookingRepository;
        _slotRepository = slotRepository;
    }
    
    public async Task<CreateBookingResponse> CreateAsync(CreateBookingRequest request)
    {
        var slot = await _slotRepository.GetByIdAsync(request.SlotId)
                   ?? throw new KeyNotFoundException("Slot not found.");
        
        int count = slot.Bookings.Count;
        if (count >= slot.Field.Capacity)
            throw new InvalidOperationException("Slot is full.");
        
        var booking = request.MapToBooking();
        var created = await _bookingRepository.CreateAsync(booking);
        return created.MapToCreateBookingResponse();
    }
    
    public async Task<IEnumerable<CreateBookingResponse>> GetAllAsync()
    {
        var bookings = await _bookingRepository.GetAllAsync();
        return bookings.Select(b => b.MapToCreateBookingResponse());
    }
    
    public async Task<CreateBookingResponse> GetByIdAsync(Guid id)
    {
        var booking = await _bookingRepository.GetByIdAsync(id);
        
        if (booking is null)
        {
            throw new KeyNotFoundException("Booking not found.");
        }
        
        return booking.MapToCreateBookingResponse();
    }
    
    public async Task<IEnumerable<CreateBookingResponse>> GetByUserIdAsync(Guid userId)
    {
        var bookings = await _bookingRepository.GetByUserIdAsync(userId);
        return bookings.Select(b => b.MapToCreateBookingResponse());
    }
    
    public async Task<IEnumerable<CreateBookingResponse>> GetBySlotIdAsync(Guid slotId)
    {
        var bookings = await _bookingRepository.GetBySlotIdAsync(slotId);
        return bookings.Select(b => b.MapToCreateBookingResponse());
    }
    
    public async Task<UpdateBookingResponse> UpdateAsync(Guid id, UpdateBookingRequest request)
    {
        var booking = request.MapToBooking(id);
        var updated = await _bookingRepository.UpdateAsync(booking);
        return updated.MapToUpdateBookingResponse();
    }
    
    public async Task DeleteAsync(Guid id)
    {
        await _bookingRepository.DeleteAsync(id);
    }
} 