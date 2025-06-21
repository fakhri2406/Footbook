using FluentValidation;
using Footbook.Core.DTOs.Requests.Slot;

namespace Footbook.Infrastructure.Validators.Slot;

public class CreateSlotRequestValidator : AbstractValidator<CreateSlotRequest>
{
    public CreateSlotRequestValidator()
    {
        RuleFor(x => x.StartTime)
            .NotEmpty().WithMessage("Start time is required.");
        
        RuleFor(x => x.EndTime)
            .NotEmpty().WithMessage("End time is required.")
            .GreaterThan(x => x.StartTime).WithMessage("End time must be after start time.");
        
        RuleFor(x => x.FieldId)
            .NotEmpty().WithMessage("Field ID is required.");
        
        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Invalid slot status.");
    }
} 