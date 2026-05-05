

using FluentValidation;
using SecureOps.Application.DTO;

namespace SecureOps.Application.Validators
{
    public class IncidentDtoValidator : AbstractValidator<IncidentDTO>
    {
        public IncidentDtoValidator()
        {
            RuleFor(x => x.IncidentCategoryId).NotEmpty();
            RuleFor(x => x.IncidentSeverityId).NotEmpty();
            RuleFor(x => x.Narrative).NotEmpty().MaximumLength(2000);
        }
    }
}
