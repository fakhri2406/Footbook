namespace Footbook.Core.DTOs.Requests.Booking;

public record BookingRequest(Guid UserId, Guid TimeWindowId);