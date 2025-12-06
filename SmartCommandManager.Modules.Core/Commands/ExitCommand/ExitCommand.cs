namespace SmartCommandManager.Modules.Core.Commands.ExitCommand
{
    public class ExitCommand : BaseCommand
    {
        public override string Name => "exit";
        public override string Description => "Close application";
        public override IEnumerable<string> NLPatterns { get; } = ["exit", "quit", "leave"];

        public ExitCommand(IFileSystemService fs, CommandContext context) : base(fs, context)
        {
        }

        public override CommandResult Execute(string[] args)
        {
            return new CommandResult() { Status = CommandStatus.Exit, Message = "Exiting..." };
        }
    }
}
