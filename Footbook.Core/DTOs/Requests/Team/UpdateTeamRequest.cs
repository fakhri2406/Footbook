namespace Footbook.Core.DTOs.Requests.Team;

public record UpdateTeamRequest(string Name, ICollection<Guid> UserIds);