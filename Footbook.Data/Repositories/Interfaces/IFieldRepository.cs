using Footbook.Data.Models;

namespace Footbook.Data.Repositories.Interfaces;

public interface IFieldRepository
{
    Task<Field> CreateAsync(Field field);
    Task<IEnumerable<Field>> GetAllAsync();
    Task<Field?> GetByIdAsync(Guid id);
    Task<IEnumerable<Field>> GetByStadiumIdAsync(Guid stadiumId);
    Task<Field> UpdateAsync(Field field);
    Task DeleteAsync(Guid id);
} 