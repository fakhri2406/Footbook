using Footbook.Core.DTOs.Requests.Team;
using Footbook.Core.DTOs.Responses.Team;

namespace Footbook.Infrastructure.Services.Interfaces;

public interface ITeamService
{
    Task<CreateTeamResponse> CreateAsync(CreateTeamRequest request);
    Task<IEnumerable<CreateTeamResponse>> GetAllAsync();
    Task<CreateTeamResponse> GetByIdAsync(Guid id);
    Task<IEnumerable<Guid>> GetMembersAsync(Guid teamId);
    Task<UpdateTeamResponse> UpdateAsync(Guid id, UpdateTeamRequest request);
    Task AddMembersAsync(Guid teamId, IEnumerable<Guid> userIds);
    Task RemoveMemberAsync(Guid teamId, Guid userId);
    Task DeleteAsync(Guid id);
} 