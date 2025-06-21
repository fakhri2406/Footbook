using FluentValidation;
using Footbook.Core.DTOs.Requests.Field;

namespace Footbook.Infrastructure.Validators.Field;

public class UpdateFieldRequestValidator : AbstractValidator<UpdateFieldRequest>
{
    public UpdateFieldRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Field name is required.")
            .MaximumLength(100).WithMessage("Field name must not exceed 100 characters.");
        
        RuleFor(x => x.FieldType)
            .IsInEnum().WithMessage("Invalid field type.");
        
        RuleFor(x => x.StadiumId)
            .NotEmpty().WithMessage("Stadium ID is required.");
        
        RuleFor(x => x.Capacity)
            .GreaterThan(0).WithMessage("Capacity must be greater than zero.");
    }
} 