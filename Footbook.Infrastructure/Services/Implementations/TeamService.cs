using FluentValidation;
using Footbook.Core.DTOs.Requests.Team;
using Footbook.Core.DTOs.Responses.Team;
using Footbook.Data.Repositories.Interfaces;
using Footbook.Infrastructure.Helpers;
using Footbook.Infrastructure.Services.Interfaces;
using Footbook.Data.Models;

namespace Footbook.Infrastructure.Services.Implementations;

public class TeamService : ITeamService
{
    private readonly ITeamRepository _teamRepository;
    private readonly ITeamMemberRepository _teamMemberRepository;
    private readonly IValidator<CreateTeamRequest> _createTeamValidator;
    private readonly IValidator<UpdateTeamRequest> _updateTeamValidator;
    
    public TeamService(
        ITeamRepository teamRepository,
        ITeamMemberRepository teamMemberRepository,
        IValidator<CreateTeamRequest> createTeamValidator,
        IValidator<UpdateTeamRequest> updateTeamValidator)
    {
        _teamRepository = teamRepository;
        _teamMemberRepository = teamMemberRepository;
        _createTeamValidator = createTeamValidator;
        _updateTeamValidator = updateTeamValidator;
    }
    
    public async Task<CreateTeamResponse> CreateAsync(CreateTeamRequest request)
    {
        await _createTeamValidator.ValidateAndThrowAsync(request);
        
        var team = request.MapToTeam();
        var created = await _teamRepository.CreateAsync(team);
        
        foreach (var userId in request.UserIds)
        {
            await _teamMemberRepository.CreateMemberAsync(
                new TeamMember { TeamId = created.Id, UserId = userId });
        }
        
        return created.MapToCreateTeamResponse();
    }
    
    public async Task<IEnumerable<CreateTeamResponse>> GetAllAsync()
    {
        var teams = await _teamRepository.GetAllAsync();
        return teams.Select(t => t.MapToCreateTeamResponse());
    }
    
    public async Task<CreateTeamResponse> GetByIdAsync(Guid id)
    {
        var team = await _teamRepository.GetByIdAsync(id);
        
        if (team is null)
        {
            throw new KeyNotFoundException("Team not found.");
        }
        
        return team.MapToCreateTeamResponse();
    }
    
    public async Task<IEnumerable<Guid>> GetMembersAsync(Guid teamId)
    {
        return (await _teamMemberRepository.GetMembersAsync(teamId)).Select(u => u.Id);
    }
    
    public async Task<UpdateTeamResponse> UpdateAsync(Guid id, UpdateTeamRequest request)
    {
        await _updateTeamValidator.ValidateAndThrowAsync(request);
        
        var team = request.MapToTeam(id);
        var updated = await _teamRepository.UpdateAsync(team);
        
        var existingMembers = await _teamMemberRepository.GetMembersAsync(id);
        foreach (var user in existingMembers)
        {
            await _teamMemberRepository.RemoveMemberAsync(id, user.Id);
        }

        foreach (var userId in request.UserIds)
        {
            await _teamMemberRepository.CreateMemberAsync(
                new TeamMember { TeamId = id, UserId = userId });
        }
        
        return updated.MapToUpdateTeamResponse();
    }
    
    public async Task AddMembersAsync(Guid teamId, IEnumerable<Guid> userIds)
    {
        foreach (var userId in userIds)
        {
            await _teamMemberRepository.CreateMemberAsync(
                new TeamMember { TeamId = teamId, UserId = userId });
        }
    }
    
    public async Task RemoveMemberAsync(Guid teamId, Guid userId)
    {
        await _teamMemberRepository.RemoveMemberAsync(teamId, userId);
    }
    
    public async Task DeleteAsync(Guid id)
    {
        await _teamRepository.DeleteAsync(id);
    }
} 