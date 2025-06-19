using Footbook.Data.Models;

namespace Footbook.Infrastructure.Tokens;

public interface ITokenGenerator
{
    string GenerateAccessToken(User user);
    string GenerateRefreshToken();
}
