using SmartCommandManager.Domain.Commands.Base;
using SmartCommandManager.Domain.Commands.Models;
using System.Text;

namespace SmartCommandManager.Modules.Core.Commands.HelpCommand
{
    public class HelpCommand : BaseCommand<Unit>
    {
        public override CommandInfo CommandInfo { get; } = new("help", "Help for application.", CommandCategory.Core);

        private readonly Func<IEnumerable<CommandInfo>> _getCommands;

        public HelpCommand(Func<IEnumerable<CommandInfo>> getCommands)
        {
            _getCommands = getCommands;
        }

        public override CommandResult Execute(Unit args)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("--help: \n");
            foreach (var cmd in _getCommands()) {
                sb.Append($"{cmd.Name} - {cmd.Description}" + "\n");
            }
            return new CommandResult() { Status = CommandStatus.Success, Message = sb.ToString() };
        }
    }
}
