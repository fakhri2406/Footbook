using System.ComponentModel.DataAnnotations;
using Footbook.Core.Enums;

namespace Footbook.Data.Models;

public class User
{
    [Key]
    public Guid Id { get; set; }
    
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string PasswordSalt { get; set; } = null!;
    public SkillLevel SkillLevel { get; set; }
    public string? ProfilePictureUrl { get; set; } = null!;
    public bool IsBanned { get; set; }
    public DateTime? BannedUntil { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? LastLoginAt { get; set; }
    
    public Guid RoleId { get; set; }
    public Role? Role { get; set; }
    
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    public ICollection<TeamMember> TeamMembers { get; set; } = new List<TeamMember>();
} 