using Microsoft.IdentityModel.Tokens;

namespace Footbook.Infrastructure.Tokens;

public class JwtOptions
{
    public string Issuer { get; set; } = null!;
    public string Audience { get; set; } = null!;
    public TimeSpan AccessValidFor { get; set; }
    public SigningCredentials SigningCredentials { get; set; } = null!;
}