using System.ComponentModel.DataAnnotations;

namespace Footbook.Data.Models;

public class Team
{
    [Key]
    public Guid Id { get; set; }
    
    public string Name { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    
    public Guid CreatedByUserId { get; set; }
    public User CreatedByUser { get; set; } = null!;
    
    public ICollection<TeamMember> TeamMembers { get; set; } = new List<TeamMember>();
} 