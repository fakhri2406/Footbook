using FluentValidation;
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
    private readonly IValidator<CreateBookingRequest> _createBookingValidator;
    private readonly IValidator<UpdateBookingRequest> _updateBookingValidator;
    
    public BookingService(
        IBookingRepository bookingRepository,
        ISlotRepository slotRepository,
        IValidator<CreateBookingRequest> createBookingValidator,
        IValidator<UpdateBookingRequest> updateBookingValidator)
    {
        _bookingRepository = bookingRepository;
        _slotRepository = slotRepository;
        _createBookingValidator = createBookingValidator;
        _updateBookingValidator = updateBookingValidator;
    }
    
    public async Task<BookingResponse> CreateAsync(CreateBookingRequest request)
    {
        await _createBookingValidator.ValidateAndThrowAsync(request);
        
        var slot = await _slotRepository.GetByIdAsync(request.SlotId)
                   ?? throw new KeyNotFoundException("Slot not found.");
        
        int count = slot.Bookings.Count;
        if (count >= slot.Field.Capacity)
            throw new InvalidOperationException("Slot is full.");
        
        var booking = request.MapToBooking();
        var created = await _bookingRepository.CreateAsync(booking);
        return created.MapToBookingResponse();
    }
    
    public async Task<IEnumerable<BookingResponse>> GetAllAsync()
    {
        var bookings = await _bookingRepository.GetAllAsync();
        return bookings.Select(b => b.MapToBookingResponse());
    }
    
    public async Task<BookingResponse> GetByIdAsync(Guid id)
    {
        var booking = await _bookingRepository.GetByIdAsync(id);
        
        if (booking is null)
        {
            throw new KeyNotFoundException("Booking not found.");
        }
        
        return booking.MapToBookingResponse();
    }
    
    public async Task<IEnumerable<BookingResponse>> GetByUserIdAsync(Guid userId)
    {
        var bookings = await _bookingRepository.GetByUserIdAsync(userId);
        return bookings.Select(b => b.MapToBookingResponse());
    }
    
    public async Task<IEnumerable<BookingResponse>> GetBySlotIdAsync(Guid slotId)
    {
        var bookings = await _bookingRepository.GetBySlotIdAsync(slotId);
        return bookings.Select(b => b.MapToBookingResponse());
    }
    
    public async Task<BookingResponse> UpdateAsync(Guid id, UpdateBookingRequest request)
    {
        await _updateBookingValidator.ValidateAndThrowAsync(request);
        
        var booking = request.MapToBooking(id);
        var updated = await _bookingRepository.UpdateAsync(booking);
        return updated.MapToBookingResponse();
    }
    
    public async Task DeleteAsync(Guid id)
    {
        await _bookingRepository.DeleteAsync(id);
    }
} 