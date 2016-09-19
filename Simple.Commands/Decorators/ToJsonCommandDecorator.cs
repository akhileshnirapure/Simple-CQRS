using System;
using Newtonsoft.Json;
using Simple.Commands.Handlers;

namespace Simple.Commands.Decorators
{
    public class ToJsonCommandDecorator<TCommand,TResult> : ICommandHandler<TCommand, TResult>
    {
        private readonly ICommandHandler<TCommand, TResult> _innerCommandHandler;

        public ToJsonCommandDecorator(ICommandHandler<TCommand,TResult> innerCommandHandler)
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