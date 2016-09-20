namespace Simple.Commands
{
    public class FullNameCommand : ICommand
    {
        public FullNameCommand()
        {
            FirstNameCommand = new FirstNameCommand();
            LastNameCommand = new LastNameCommand();
        }
        public FirstNameCommand FirstNameCommand { get; set; }
        public LastNameCommand LastNameCommand { get; set; }
    }
}