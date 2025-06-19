using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Footbook.Data.Models;

namespace Footbook.Infrastructure.Tokens;

public class TokenGenerator : ITokenGenerator
{
    private readonly JwtOptions _jwtOptions;
    
    public TokenGenerator(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }
    
    public string GenerateAccessToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim("id", user.Id.ToString()),
        };

        if (user.UserRoles != null)
        {
            claims.AddRange(
                user.UserRoles.Select(ur =>
                    new Claim(ClaimTypes.Role, ur.Role.Name))
            );
        }

        claims.Add(new Claim("firstName", user.FirstName));
        claims.Add(new Claim("lastName", user.LastName));
        claims.Add(new Claim("skillLevel", user.SkillLevel.ToString()));

        var token = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.Add(_jwtOptions.AccessValidFor),
            signingCredentials: _jwtOptions.SigningCredentials,
            claims: claims
        );
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
    public string GenerateRefreshToken()
    {
        return Guid.NewGuid().ToString();
    }
}
