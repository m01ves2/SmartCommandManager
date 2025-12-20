using SmartCommandManager.Domain.Commands.Base;
using SmartCommandManager.Domain.Commands.Models;
using SmartCommandManager.NLP.Intent.Models;

namespace SmartCommandManager.Modules.Core.Commands.ExitCommand
{
    public sealed class ExitCommand : BaseCommand<Unit>
    {
        public override CommandInfo CommandInfo { get; }  = new("exit", "Exit from application.", CommandCategory.Core);

        public override CommandResult Execute(Unit args)
        {
            return new CommandResult() { Status = CommandStatus.Exit, Message = "Exiting..." };
        }
    }
}
