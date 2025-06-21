using FluentValidation;
using Footbook.Core.DTOs.Requests.Booking;

namespace Footbook.Infrastructure.Validators.Booking;

public class UpdateBookingRequestValidator : AbstractValidator<UpdateBookingRequest>
{
    public UpdateBookingRequestValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required.");
        
        RuleFor(x => x.SlotId)
            .NotEmpty().WithMessage("Slot ID is required.");
    }
} 