using FluentValidation;
using InventoryV2.Dtos.AuthDtos.Requests;
using InventoryV2.Seeders;

namespace InventoryV2.Dtos.AuthDtos.Validators
{
    public class RegisterDtoValidator:AbstractValidator<RegisterDto>
    {
        //Localization : ValidationMessages.ar.resx
        // IValidator<RegisterDto> _validtor  search about clean way
        public RegisterDtoValidator()
        {

            // Username (no spaces allowed)
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Username is required")
                .MinimumLength(3).WithMessage("Username must be at least 3 characters long");

            // Email
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format");

            // Password
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter")
                .Matches("[0-9]").WithMessage("Password must contain at least one number")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character");

                // Confirm Password
                RuleFor(x => x.ConfirmPassword)
                    .Equal(x => x.Password).WithMessage("Passwords do not match");

                // Role (must be Employee, Manager, or Owner)
                RuleFor(x => x.Role)
                    .NotEmpty().WithMessage("Role is required")
                    .Must(role => SystemRoles.All.Contains(role))
                    .WithMessage($"Role must be one of: {string.Join(", ", SystemRoles.All)}");

                RuleFor(x => x.UserKey)
               .NotNull().WithMessage("UserKey is required.");

                RuleFor(x => x.Otp)
               .NotEmpty().WithMessage("OTP is required.")
               .Length(6).WithMessage("OTP must be exactly 6 digits.")
               .Matches(@"^\d{6}$").WithMessage("OTP must contain only numbers.");

        }
    }
}
