using FluentValidation;
using Footbook.Core.DTOs.Requests.Stadium;

namespace Footbook.Infrastructure.Validators.Stadium;

public class CreateStadiumRequestValidator : AbstractValidator<CreateStadiumRequest>
{
    public CreateStadiumRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Stadium name is required.")
            .MaximumLength(100).WithMessage("Stadium name must not exceed 100 characters.");
        
        RuleFor(x => x.Branch)
            .IsInEnum().WithMessage("Invalid branch.");
        
        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Address is required.");
        
        RuleFor(x => x.Latitude)
            .InclusiveBetween(-90, 90).WithMessage("Latitude must be between -90 and 90.");
        
        RuleFor(x => x.Longitude)
            .InclusiveBetween(-180, 180).WithMessage("Longitude must be between -180 and 180.");
    }
} 