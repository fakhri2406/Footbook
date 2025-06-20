using Footbook.Core.DTOs.Requests.Auth;
using Footbook.Core.DTOs.Responses.Auth;

namespace Footbook.Infrastructure.Services.Interfaces;

public interface IAuthService
{
    Task<AuthResponse> SignupAsync(SignupRequest request);
    Task<AuthResponse> LoginAsync(LoginRequest request);
    Task<AuthResponse> RefreshTokenAsync(RefreshTokenRequest request);
    Task LogoutAsync(LogoutRequest request);
} 