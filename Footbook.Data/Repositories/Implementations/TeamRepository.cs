using Microsoft.EntityFrameworkCore;
using Footbook.Data.DataAccess;
using Footbook.Data.Models;
using Footbook.Data.Repositories.Interfaces;

namespace Footbook.Data.Repositories.Implementations;

public class TeamRepository : ITeamRepository
{
    private readonly AppDbContext _context;
    public TeamRepository(AppDbContext context) => _context = context;
    
    public async Task<Team> CreateAsync(Team team)
    {
        _context.Teams.Add(team);
        await _context.SaveChangesAsync();
        return team;
    }
    
    public async Task<IEnumerable<Team>> GetAllAsync()
    {
        return await _context.Teams
            .Include(t => t.TeamMembers)
            .ToListAsync();
    }
    
    public async Task<Team?> GetByIdAsync(Guid id)
    {
        return await _context.Teams
            .Include(t => t.TeamMembers)
            .FirstOrDefaultAsync(t => t.Id == id);
    }
    
    public async Task<IEnumerable<Team>> GetByUserIdAsync(Guid userId)
    {
        return await _context.TeamMembers
            .Where(tm => tm.UserId == userId)
            .Select(tm => tm.Team)
            .Include(t => t.TeamMembers)
            .ToListAsync();
    }
    
    public async Task<Team> UpdateAsync(Team team)
    {
        _context.Teams.Update(team);
        await _context.SaveChangesAsync();
        return team;
    }
    
    public async Task DeleteAsync(Guid id)
    {
        var team = await _context.Teams.FindAsync(id);
        if (team is not null)
        {
            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();
        }
    }
} 