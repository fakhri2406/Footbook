using Footbook.Core.DTOs.Requests.User;
using Footbook.Core.DTOs.Responses.User;

namespace Footbook.Infrastructure.Services.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserResponse>> GetAllAsync();
    Task<UserResponse> GetByIdAsync(Guid id);
    Task<UserResponse> UpdateAsync(Guid id, UpdateUserRequest request);
    Task DeleteAsync(Guid id);
}