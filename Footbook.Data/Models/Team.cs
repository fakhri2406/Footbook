namespace Footbook.Data.Models;

public class Team
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public Guid CreatedByUserId { get; set; }
    public User CreatedByUser { get; set; } = null!;
    public DateTime CreatedAt { get; set; }

    public ICollection<TeamMember> TeamMembers { get; set; } = new List<TeamMember>();
} 