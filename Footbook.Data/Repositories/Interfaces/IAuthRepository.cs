using Footbook.Data.Models;

namespace Footbook.Data.Repositories.Interfaces;

public interface IAuthRepository
{
    Task CreateAsync(User user);
    Task CreateRefreshTokenAsync(RefreshToken refreshToken);
    Task<RefreshToken?> GetRefreshTokenAsync(string token);
    Task RemoveRefreshTokenAsync(string token);
    Task RemoveAllRefreshTokensAsync(Guid userId);
} 