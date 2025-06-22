namespace Footbook.Core.DTOs.Responses.Team;

public record TeamResponse(string Name, Guid CreatedByUserId, ICollection<Guid> UserIds);