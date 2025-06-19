using Footbook.Core.Enums;

namespace Footbook.Data.Models;

public class TimeWindow
{
    public Guid Id { get; set; }
    public Guid FieldId { get; set; }
    public Field Field { get; set; } = null!;

    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public WindowStatus Status { get; set; }

    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
} 