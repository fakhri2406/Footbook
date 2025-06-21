using System.ComponentModel.DataAnnotations;
using Footbook.Core.Enums;

namespace Footbook.Data.Models;

public class Slot
{
    [Key]
    public Guid Id { get; set; }
    
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public SlotStatus Status { get; set; }
    
    public Guid FieldId { get; set; }
    public Field Field { get; set; } = null!;
    
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
} 