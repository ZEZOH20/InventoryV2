using FluentValidation;
using InventoryV2.Dtos.AuthDtos.Requests;

namespace InventoryV2.Dtos.AuthDtos.Validators.SendVerificationEmail
{
    public class SendVerificationEmailRqDtoValidator:AbstractValidator<SendVerificationEmailRqDto>
    {
        public SendVerificationEmailRqDtoValidator() {

             RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email format");

        }
    }
}
