using FluentValidation;
using InventoryV2.Dtos.AuthDtos.Requests;

namespace InventoryV2.Dtos.AuthDtos.Validators
{
    public class SendVerificationEmailDtoValidator:AbstractValidator<SendVerificationEmailRqDto>
    {
        public SendVerificationEmailDtoValidator() {

             RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email format");

        }
    }
}
