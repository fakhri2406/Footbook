namespace Footbook.Core.DTOs.Responses.Team;

public record CreateTeamResponse(string Name, Guid CreatedByUserId, ICollection<Guid> UserIds);