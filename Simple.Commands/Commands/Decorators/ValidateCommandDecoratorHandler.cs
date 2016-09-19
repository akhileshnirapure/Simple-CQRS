using System;
using FluentValidation;

namespace Simple.Commands.Commands.Decorators
{
    public class ValidateCommandDecoratorHandler<TCommand, TResult> : ICommandHandler<TCommand, TResult>
    {
        private readonly ICommandHandler<TCommand, TResult> _innerCommandHandler;
        private readonly IValidator<TCommand> _validator;

        public ValidateCommandDecoratorHandler(ICommandHandler<TCommand, TResult> innerCommandHandler,IValidator<TCommand> validator)
        {
            if (innerCommandHandler == null) throw new ArgumentNullException(nameof(innerCommandHandler));
            if (validator == null) throw new ArgumentNullException(nameof(validator));
            _innerCommandHandler = innerCommandHandler;
            _validator = validator;
        }

        public TResult Execute(TCommand command)
        {
            Console.WriteLine("Validation CommandHandler Start");

            _validator.ValidateAndThrow(command, ruleSet: "myset");
            Console.WriteLine("CommandModel is Valid");
            var result = _innerCommandHandler.Execute(command);
            Console.WriteLine("Validation CommandHandler End");
            return result;

        }
    }
}