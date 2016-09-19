using System;
using FluentValidation;
using Simple.Commands.Handlers;

namespace Simple.Commands.Decorators
{
    public class ValidateCommandDecorator<TCommand, TResult> : ICommandHandler<TCommand, TResult>
    {
        private readonly ICommandHandler<TCommand, TResult> _innerCommandHandler;
        private readonly IValidator<TCommand> _validator;

        public ValidateCommandDecorator(ICommandHandler<TCommand, TResult> innerCommandHandler,IValidator<TCommand> validator)
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