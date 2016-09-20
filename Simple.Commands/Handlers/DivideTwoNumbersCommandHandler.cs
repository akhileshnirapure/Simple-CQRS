using System;
using Simple.Models;

namespace Simple.Commands.Handlers
{
    public class DivideTwoNumbersCommandHandler : ICommandHandler<DivideTwoNumbersCommand, AdditionResult>
    {
        public AdditionResult Execute(DivideTwoNumbersCommand command)
        {
            Console.WriteLine("Core CommandHandler Start");

            var result = new AdditionResult { Total = command.Num1 / command.Num2 };

            Console.WriteLine("Command Executed Successfully");

            Console.WriteLine("Core CommandHandler End");

            return result;
        }
    }
}