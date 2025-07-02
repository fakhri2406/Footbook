using System.Security;
using FluentValidation;
using Footbook.Core.DTOs.Requests.Auth;
using Footbook.Core.DTOs.Responses.Auth;
using Footbook.Data.Models;
using Footbook.Data.Repositories.Interfaces;
using Footbook.Infrastructure.ExternalServices.Cloudinary;
using Footbook.Infrastructure.Services.Interfaces;
using Footbook.Infrastructure.Tokens;
using Footbook.Infrastructure.Helpers;

namespace Footbook.Infrastructure.Services.Implementations;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepository;
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly ITokenGenerator _tokenGenerator;
    private readonly ICloudinaryService _cloudinaryService;
    private readonly IValidator<SignupRequest> _signupValidator;
    private readonly IValidator<LoginRequest> _loginValidator;
    private readonly IValidator<RefreshTokenRequest> _refreshTokenValidator;
    private readonly IValidator<LogoutRequest> _logoutValidator;
    
    public AuthService(
        IAuthRepository authRepository,
        IUserRepository userRepository,
        IRoleRepository roleRepository,
        ITokenGenerator tokenGenerator,
        ICloudinaryService cloudinaryService,
        IValidator<SignupRequest> signupValidator,
        IValidator<LoginRequest> loginValidator,
        IValidator<RefreshTokenRequest> refreshTokenValidator,
        IValidator<LogoutRequest> logoutValidator)
    {
        _authRepository = authRepository;
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _tokenGenerator = tokenGenerator;
        _cloudinaryService = cloudinaryService;
        _signupValidator = signupValidator;
        _loginValidator = loginValidator;
        _refreshTokenValidator = refreshTokenValidator;
        _logoutValidator = logoutValidator;
    }
    
    public async Task<AuthResponse> SignupAsync(SignupRequest request)
    {
        await _signupValidator.ValidateAndThrowAsync(request);
        
        if (await _userRepository.GetByEmailAsync(request.Email) != null)
        {
            throw new ArgumentException("Email already exists.");
        }
        
        if (await _userRepository.GetByPhoneNumberAsync(request.PhoneNumber) != null)
        {
            throw new ArgumentException("Phone number already exists.");
        }
        
        var role = await _roleRepository.GetByNameAsync("User")
                   ?? throw new InvalidOperationException("Default user role not found.");
        var user = request.MapToUser(role.Id);
        
        var image = request.ProfilePicture;
        if (image is not null)
        {
            var uploadResult = await _cloudinaryService.UploadImageAsync(image, "users");
            user.ProfilePictureUrl = uploadResult.SecureUrl.ToString();
        }
        
        await _authRepository.CreateAsync(user);
        
        var accessToken = _tokenGenerator.GenerateAccessToken(user);
        var refreshTokenValue = _tokenGenerator.GenerateRefreshToken();
        var refreshToken = new RefreshToken
        {
            Token = refreshTokenValue,
            UserId = user.Id,
            ExpiresAt = DateTime.UtcNow.AddDays(7)
        };
        
        await _authRepository.CreateRefreshTokenAsync(refreshToken);
        
        return new AuthResponse(accessToken, refreshTokenValue);
    }
    
    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        await _loginValidator.ValidateAndThrowAsync(request);
        
        var user = await _userRepository.GetByEmailAsync(request.Email)
                   ?? throw new KeyNotFoundException("Invalid email or password.");
        
        var hashed = Hasher.HashPassword(request.Password + user.PasswordSalt);
        if (hashed != user.PasswordHash)
        {
            throw new KeyNotFoundException("Invalid email or password.");
        }
        
        user.LastLoginAt = DateTime.UtcNow;
        await _userRepository.UpdateAsync(user);
        
        var accessToken = _tokenGenerator.GenerateAccessToken(user);
        var refreshTokenValue = _tokenGenerator.GenerateRefreshToken();
        var refreshToken = new RefreshToken
        {
            Token = refreshTokenValue,
            UserId = user.Id,
            ExpiresAt = DateTime.UtcNow.AddDays(7)
        };
        
        await _authRepository.CreateRefreshTokenAsync(refreshToken);
        
        return new AuthResponse(accessToken, refreshTokenValue);
    }
    
    public async Task<AuthResponse> RefreshTokenAsync(RefreshTokenRequest request)
    {
        await _refreshTokenValidator.ValidateAndThrowAsync(request);
        
        var existing = await _authRepository.GetRefreshTokenAsync(request.RefreshToken)
                       ?? throw new KeyNotFoundException("Invalid refresh token.");

        if (existing.ExpiresAt < DateTime.UtcNow)
        {
            throw new SecurityException("Refresh token has expired.");
        }

        var user = existing.User;
        await _authRepository.RemoveRefreshTokenAsync(existing.Token);

        var accessToken = _tokenGenerator.GenerateAccessToken(user);
        var newRefreshTokenValue = _tokenGenerator.GenerateRefreshToken();
        var newRefreshToken = new RefreshToken
        {
            Token = newRefreshTokenValue,
            UserId = user.Id,
            ExpiresAt = DateTime.UtcNow.AddDays(7)
        };
        
        await _authRepository.CreateRefreshTokenAsync(newRefreshToken);
        
        return new AuthResponse(accessToken, newRefreshTokenValue);
    }
    
    public async Task LogoutAsync(LogoutRequest request)
    {
        await _logoutValidator.ValidateAndThrowAsync(request);
        
        await _authRepository.RemoveAllRefreshTokensAsync(request.UserId);
    }
} 