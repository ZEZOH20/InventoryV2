using FluentValidation;
using InventoryV2.Dtos.ProfileDto.Requests;

namespace InventoryV2.Dtos.ProfileDto.Validations;

public class ProfileUpdateValidator:AbstractValidator<UpdateUserProfileDto>
{
    public ProfileUpdateValidator()
    {
        RuleFor(x => x)
            .Must(x => !string.IsNullOrWhiteSpace(x.UserName) || !string.IsNullOrWhiteSpace(x.PhoneNumber))
            .WithMessage("At least one field must be provided.");

        RuleFor(x => x.UserName)
            .Must(u => !string.IsNullOrWhiteSpace(u))
            .WithMessage("Username cannot be empty.")
            .MinimumLength(3).WithMessage("UserName must be at least 3 characters.")
            .MaximumLength(50).WithMessage("UserName must be at most 50 characters.")
            .Matches(@"^[a-zA-Z0-9_.-]+$").WithMessage("UserName contains invalid characters.")
            .When(x => !string.IsNullOrWhiteSpace(x.UserName));

        RuleFor(x => x.PhoneNumber)
            .Must(u => !string.IsNullOrWhiteSpace(u))
            .WithMessage("Username cannot be empty.")
            .Matches(@"^\+?\d{7,15}$")
            .WithMessage("PhoneNumber must be a valid phone number (7-15 digits, optional leading +).")
            .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber));
    }
}