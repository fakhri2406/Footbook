using Footbook.Data.Models;

namespace Footbook.Data.Repositories.Interfaces;

public interface ITeamRepository
{
    Task<Team> CreateAsync(Team team);
    Task<IEnumerable<Team>> GetAllAsync();
    Task<Team?> GetByIdAsync(Guid id);
    Task<IEnumerable<Team>> GetByUserIdAsync(Guid userId);
    Task<Team> UpdateAsync(Team team);
    Task DeleteAsync(Guid id);
} 