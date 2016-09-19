using System;
using Simple.Models;

namespace Simple.Commands.Handlers
{
    public class AddTwoNumbersCommandHandler : ICommandHandler<AddTwoNumbersCommand, AdditionResult>
    {
        public AdditionResult Execute(AddTwoNumbersCommand command)
        {
            Console.WriteLine("Core CommandHandler Start");

            var result = new AdditionResult { Total = command.Number1 + command.Number2 };

            Console.WriteLine("Command Executed Successfully");

            Console.WriteLine("Core CommandHandler End");

            return result;
        }
    }
}