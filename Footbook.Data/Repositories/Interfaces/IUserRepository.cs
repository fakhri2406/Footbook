using Footbook.Data.Models;

namespace Footbook.Data.Repositories.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllAsync();
    Task<User?> GetByIdAsync(Guid id);
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByPhoneNumberAsync(string phoneNumber);
    Task<User> UpdateAsync(User user);
    Task DeleteAsync(Guid id);
}