using FluentValidation;
using Simple.Commands;

namespace Simple.Validations
{
    public class FirstNameValidator : AbstractValidator<FirstNameCommand>
    {
        public FirstNameValidator()
        {
            RuleFor(p => p.FirstName).NotEmpty().WithMessage("{PropertyName} Should have value");
        }
    }
}