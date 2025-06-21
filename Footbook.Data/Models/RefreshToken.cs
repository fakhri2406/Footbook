using System.ComponentModel.DataAnnotations;

namespace Footbook.Data.Models;

public class RefreshToken
{
    [Key]
    public Guid Id { get; set; }
    
    public string Token { get; set; } = null!;
    public DateTime ExpiresAt { get; set; }
    public DateTime? RevokedAt { get; set; }
    
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
} 