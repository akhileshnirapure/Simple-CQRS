using System;

namespace Simple.Commands.Commands.Decorators
{
    public class CommandLogDecoratorHandler<TCommand, TResult> : ICommandHandler<TCommand, TResult>
    {
        private readonly ICommandHandler<TCommand, TResult> _innerCommandHandler;

        public CommandLogDecoratorHandler(ICommandHandler<TCommand, TResult> innerCommandHandler)
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