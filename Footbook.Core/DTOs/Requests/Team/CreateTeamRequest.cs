namespace Footbook.Core.DTOs.Requests.Team;

public record CreateTeamRequest(string Name, Guid CreatedByUserId, ICollection<Guid> UserIds);