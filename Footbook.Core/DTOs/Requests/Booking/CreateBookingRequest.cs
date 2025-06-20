namespace Footbook.Core.DTOs.Requests.Booking;

public record CreateBookingRequest(Guid UserId, Guid SlotId);