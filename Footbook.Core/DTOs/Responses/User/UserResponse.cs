using Footbook.Core.Enums;

namespace Footbook.Core.DTOs.Responses.User;

public record UserResponse (
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    SkillLevel SkillLevel,
    string? ProfilePictureUrl
);