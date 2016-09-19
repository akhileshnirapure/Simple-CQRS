using FluentValidation;
using Simple.Commands.Commands;

namespace Simple.Validations
{
    public class AddTwoNumbersValidator : AbstractValidator<AddTwoNumbersCommand>
    {
        public AddTwoNumbersValidator()
        {
            RuleSet("myset",() =>
            {
                RuleFor(p => p.Number1).GreaterThan(4).WithMessage("{PropertyName} Should be greater that {ComparisonValue} instead {PropertyValue} was provided.");
                RuleFor(p => p.Number2).GreaterThan(5).WithMessage("{PropertyName} Should be greater that {ComparisonValue} instead {PropertyValue} was provided.");
            });
            
        }
    }
}