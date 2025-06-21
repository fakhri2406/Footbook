using System.ComponentModel.DataAnnotations;
using Footbook.Core.Enums;

namespace Footbook.Data.Models;

public class Notification
{
    [Key]
    public Guid Id { get; set; }
    
    public NotificationType Type { get; set; }
    public string Payload { get; set; } = null!;
    public bool IsRead { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
} 