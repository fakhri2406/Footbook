using Microsoft.EntityFrameworkCore;
using Footbook.Data.DataAccess;
using Footbook.Data.Models;
using Footbook.Data.Repositories.Interfaces;

namespace Footbook.Data.Repositories.Implementations;

public class TeamMemberRepository : ITeamMemberRepository
{
    private readonly AppDbContext _context;
    public TeamMemberRepository(AppDbContext context) => _context = context;
    
    public async Task CreateMemberAsync(TeamMember teamMember)
    {
        _context.TeamMembers.Add(teamMember);
        await _context.SaveChangesAsync();
    }
    
    public async Task RemoveMemberAsync(Guid teamId, Guid userId)
    {
        var tm = await _context.TeamMembers.FindAsync(teamId, userId);
        if (tm is not null)
        {
            _context.TeamMembers.Remove(tm);
            await _context.SaveChangesAsync();
        }
    }
    
    public async Task<IEnumerable<User>> GetMembersAsync(Guid teamId)
    {
        return await _context.TeamMembers
            .Where(tm => tm.TeamId == teamId)
            .Select(tm => tm.User)
            .ToListAsync();
    }
    
    public async Task<IEnumerable<Team>> GetTeamsByUserAsync(Guid userId)
    {
        return await _context.TeamMembers
            .Where(tm => tm.UserId == userId)
            .Select(tm => tm.Team)
            .ToListAsync();
    }
} 