using System;

namespace Simple.Commands.Handlers
{
    public class FullNameCommandHandler : ICommandHandler<FullNameCommand, string>
    {
        public string Execute(FullNameCommand command)
        {
            Console.WriteLine("Core CommandHandler Start");

            var result = command.FirstNameCommand.FirstName + " " +command.LastNameCommand.LastName;

            Console.WriteLine("Command Executed Successfully");

            Console.WriteLine("Core CommandHandler End");

            return result;
        }
    }
}