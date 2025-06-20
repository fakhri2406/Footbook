using Footbook.Data.Models;

namespace Footbook.Data.Repositories.Interfaces;

public interface ITeamMemberRepository
{
    Task CreateMemberAsync(TeamMember teamMember);
    Task RemoveMemberAsync(Guid teamId, Guid userId);
    Task<IEnumerable<User>> GetMembersAsync(Guid teamId);
    Task<IEnumerable<Team>> GetTeamsByUserAsync(Guid userId);
} 