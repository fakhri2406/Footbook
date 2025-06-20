namespace Footbook.Core.DTOs.Responses.Auth;

public record AuthResponse (string AccessToken, string RefreshToken);