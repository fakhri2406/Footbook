namespace Footbook.Core.DTOs.Requests.Team;

public record UpdateTeamResponse(string Name, ICollection<Guid> UserIds);