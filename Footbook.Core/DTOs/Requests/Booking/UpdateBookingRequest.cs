namespace Footbook.Core.DTOs.Requests.Booking;

public record UpdateBookingRequest(Guid UserId, Guid SlotId);