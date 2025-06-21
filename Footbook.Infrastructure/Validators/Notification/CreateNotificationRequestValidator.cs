using FluentValidation;
using Footbook.Core.DTOs.Requests.Notification;

namespace Footbook.Infrastructure.Validators.Notification;

public class CreateNotificationRequestValidator : AbstractValidator<CreateNotificationRequest>
{
    public CreateNotificationRequestValidator()
    {
        RuleFor(x => x.Type)
            .IsInEnum().WithMessage("Invalid notification type.");
        
        RuleFor(x => x.Payload)
            .NotEmpty().WithMessage("Payload is required.")
            .MaximumLength(1000).WithMessage("Payload must not exceed 1000 characters.");
        
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required.");
    }
} 