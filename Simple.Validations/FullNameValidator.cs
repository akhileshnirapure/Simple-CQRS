using FluentValidation;
using Simple.Commands;

namespace Simple.Validations
{
    public class FullNameValidator : AbstractValidator<FullNameCommand>
    {
        public FullNameValidator()
        {
            RuleFor(fistname => fistname.FirstNameCommand).SetValidator(new FirstNameValidator());
            RuleFor(lastname => lastname.LastNameCommand).SetValidator(new LastNameValidator());
        }
    }
}