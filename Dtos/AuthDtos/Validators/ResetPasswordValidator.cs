using FluentValidation;
using InventoryV2.Dtos.AuthDtos.Requests;

namespace InventoryV2.Dtos.AuthDtos.Validators
{
    public class ResetPasswordValidator:AbstractValidator<ResetPasswordDto>
    {
        public ResetPasswordValidator() {

            // Email
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format");

            // NewPassword
            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("New Password is required")
                .MinimumLength(6).WithMessage("New Password must be at least 6 characters long")
                .Matches("[A-Z]").WithMessage("New Password must contain at least one uppercase letter")
                .Matches("[a-z]").WithMessage("New Password must contain at least one lowercase letter")
                .Matches("[0-9]").WithMessage("New Password must contain at least one number")
                .Matches("[^a-zA-Z0-9]").WithMessage("New Password must contain at least one special character");

            RuleFor(x => x.UserKey)
              .NotNull().WithMessage("UserKey is required.");

            RuleFor(x => x.Otp)
           .NotEmpty().WithMessage("OTP is required.")
           .Length(6).WithMessage("OTP must be exactly 6 digits.")
           .Matches(@"^\d{6}$").WithMessage("OTP must contain only numbers.");
        }
    }
}
