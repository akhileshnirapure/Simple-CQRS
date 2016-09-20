using FluentValidation;
using Simple.Commands;

namespace Simple.Validations
{
    public class LastNameValidator : AbstractValidator<LastNameCommand>
    {
        public LastNameValidator()
        {
            RuleFor(p => p.LastName).NotEmpty().WithMessage("{PropertyName} Should have value");
        }
    }
}