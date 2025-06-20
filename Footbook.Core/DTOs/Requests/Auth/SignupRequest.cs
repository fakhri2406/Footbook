using Footbook.Core.Enums;

namespace Footbook.Core.DTOs.Requests.Auth;

public record SignupRequest (
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    string Password,
    SkillLevel SkillLevel
);