using Footbook.Core.Enums;
using Microsoft.AspNetCore.Http;

namespace Footbook.Core.DTOs.Requests.User;

public record UpdateUserRequest (
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    string Password,
    SkillLevel SkillLevel,
    IFormFile? ProfilePicture
);