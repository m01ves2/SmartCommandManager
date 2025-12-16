using SmartCommandManager.Domain.Commands.Base;
using SmartCommandManager.Domain.Commands.Models;
using System.Text;

namespace SmartCommandManager.Modules.Core.Commands.HelpCommand
{
    public class HelpCommand : BaseCommand<Unit>
    {
        public override CommandInfo CommandInfo { get; } = new("help", "Help for application.", CommandCategory.Core);

        private readonly IReadOnlyList<CommandInfo> _commandsInfo;

        public HelpCommand(IReadOnlyList<CommandInfo> commandsInfo)
        {
            _commandsInfo = commandsInfo;
        }

        public override CommandResult Execute(Unit args)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("--help: \n");
            foreach (var item in _commandsInfo) {
                sb.Append($"{item.Name} - {item.Description}" + "\n");
            }
            return new CommandResult() { Status = CommandStatus.Success, Message = sb.ToString() };
        }
    }
}
