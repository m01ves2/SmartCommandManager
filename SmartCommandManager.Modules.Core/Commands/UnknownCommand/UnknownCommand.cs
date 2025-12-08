using SmartCommandManager.Domain.Commands;
using SmartCommandManager.Domain.NLP;

namespace SmartCommandManager.Modules.Core.Commands.UnknownCommand
{
    public class UnknownCommand : BaseCommand<Unit>
    {
        public override CommandInfo CommandInfo { get; } = new("unknown", "Handles unknown commands", CommandCategory.Core);

        public override IntentPattern IntentPattern { get; } = new IntentPattern("unknown", new[] { "unknown" });

        protected override CommandResult Execute(Unit args)
        {
            return new CommandResult() { Status = CommandStatus.Error, Message = $"Unknown command. Try 'help'." };
        }
    }
}
