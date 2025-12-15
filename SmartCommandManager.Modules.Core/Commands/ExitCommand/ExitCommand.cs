using SmartCommandManager.Domain.Commands;
using SmartCommandManager.NLP.Intent.Models;

namespace SmartCommandManager.Modules.Core.Commands.ExitCommand
{
    public sealed class ExitCommand : BaseCommand<Unit>
    {
        public override CommandInfo CommandInfo { get; }  = new("exit", "Exit from application.", CommandCategory.Core);

        protected override CommandResult Execute(Unit args)
        {
            return new CommandResult() { Status = CommandStatus.Exit, Message = "Exiting..." };
        }
    }

    //public class ExitCommand : FileSystemCommandBase<>
    //{
    //    public override string Name => "exit";
    //    public override string Description => "Close application";
    //    public override IEnumerable<string> NLPatterns { get; } = ["exit", "quit", "leave"];

    //    public ExitCommand(IFileSystemService fs, CommandContext context) : base(fs, context)
    //    {
    //    }

    //    public override CommandResult Execute(string[] args)
    //    {
    //        return new CommandResult() { Status = CommandStatus.Exit, Message = "Exiting..." };
    //    }
}
