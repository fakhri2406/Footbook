using System.Linq;
using FluentValidation;
using Footbook.Core.DTOs.Requests.Team;

namespace Footbook.Infrastructure.Validators.Team;

public class UpdateTeamRequestValidator : AbstractValidator<UpdateTeamRequest>
{
    public UpdateTeamRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Team name is required.")
            .MaximumLength(100).WithMessage("Team name must not exceed 100 characters.");

        RuleFor(x => x.UserIds)
            .NotNull().WithMessage("User IDs collection is required.")
            .Must(u => u.Any()).WithMessage("At least one member is required.");

        RuleForEach(x => x.UserIds)
            .NotEmpty().WithMessage("User ID cannot be empty.");
    }
} 