using Footbook.Core.Enums;

namespace Footbook.Data.Models;

public class Booking
{
    public Guid Id { get; set; }
    
    public BookingStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? CancelledAt { get; set; }
    
    public Guid SlotId { get; set; }
    public Slot Slot { get; set; } = null!;
    
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
} 