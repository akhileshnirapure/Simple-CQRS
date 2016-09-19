using System;
using Simple.Commands.Handlers;

namespace Simple.Commands.Decorators
{
    public class LogCommandDecorator<TCommand, TResult> : ICommandHandler<TCommand, TResult>
    {
        private readonly ICommandHandler<TCommand, TResult> _innerCommandHandler;

        public LogCommandDecorator(ICommandHandler<TCommand, TResult> innerCommandHandler)
        {
            if (innerCommandHandler == null) throw new ArgumentNullException(nameof(innerCommandHandler));
            _innerCommandHandler = innerCommandHandler;
        }

        public TResult Execute(TCommand command)
        {
            Console.WriteLine("Log CommandHandler Start");
            var result = _innerCommandHandler.Execute(command);
            Console.WriteLine("Log CommandHandler End");
            return result;

        }
    }
}