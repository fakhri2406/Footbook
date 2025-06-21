using FluentValidation;
using Footbook.Core.DTOs.Requests.Auth;

namespace Footbook.Infrastructure.Validators.Auth;

public class LogoutRequestValidator : AbstractValidator<LogoutRequest>
{
    public LogoutRequestValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required.");
    }
} 