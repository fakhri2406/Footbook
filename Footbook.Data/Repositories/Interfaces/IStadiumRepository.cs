using Footbook.Data.Models;

namespace Footbook.Data.Repositories.Interfaces;

public interface IStadiumRepository
{
    Task<Stadium> CreateAsync(Stadium stadium);
    Task<IEnumerable<Stadium>> GetAllAsync();
    Task<Stadium?> GetByIdAsync(Guid id);
    Task<Stadium> UpdateAsync(Stadium stadium);
    Task DeleteAsync(Guid id);
} 