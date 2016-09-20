using FluentValidation;
using Simple.Commands;

namespace Simple.Validations
{
    public class DivideTwoNumbersValidator : AbstractValidator<DivideTwoNumbersCommand>
    {
        public DivideTwoNumbersValidator()
        {
            RuleSet("myset", () =>
            {
                RuleFor(p => p.Num1).NotEmpty().WithMessage("Should have value");
                RuleFor(p => p.Num2).NotEmpty().WithMessage("Should have value");
                RuleFor(p => p.Num1).GreaterThan(0).WithMessage("Should be greater than {ComparisonValue}");
                RuleFor(p => p.Num2).NotEqual(0).WithMessage("Should not be {ComparisonValue}");

                RuleFor(p => p.Num1).GreaterThan(0).WithMessage("{PropertyName} Should be greater that {ComparisonValue} instead {PropertyValue} was provided.");
                RuleFor(p => p.Num2).GreaterThan(0).WithMessage("{PropertyName} Should be greater that {ComparisonValue} instead {PropertyValue} was provided.");
            });

        }
    }
}