using Footbook.Core.Enums;
using Microsoft.AspNetCore.Http;

namespace Footbook.Core.DTOs.Requests.Auth;

public record SignupRequest (
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    string Password,
    SkillLevel SkillLevel,
    IFormFile? ProfilePicture
);