using System;
using Newtonsoft.Json;

namespace Simple.Commands.Commands.Decorators
{
    public class CommandToJsonDecoratorHandler<TCommand,TResult> : ICommandHandler<TCommand, TResult>
    {
        private readonly ICommandHandler<TCommand, TResult> _innerCommandHandler;

        public CommandToJsonDecoratorHandler(ICommandHandler<TCommand,TResult> innerCommandHandler)
        {
            if (innerCommandHandler == null) throw new ArgumentNullException(nameof(innerCommandHandler));
            _innerCommandHandler = innerCommandHandler;
        }

        public TResult Execute(TCommand command)
        {
            Console.WriteLine("ToJSON CommandHandler Start");
            Console.WriteLine(JsonConvert.SerializeObject(command));
            var result =  _innerCommandHandler.Execute(command);
            Console.WriteLine("ToJSON CommandHandler End");
            return result;

        }
    }
}