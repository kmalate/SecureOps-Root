using FluentValidation;
using SecureOps.Application.DTO;

namespace SecureOps.Application.Validators
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator() { 
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.SSNLastFour)
                .NotEmpty()
                .Length(4)
                .Matches(@"^\d{4}$").WithMessage("SSN last four must be exactly 4 digits.");
        }
    }
}
