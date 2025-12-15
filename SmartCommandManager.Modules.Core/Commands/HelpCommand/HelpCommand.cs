using SmartCommandManager.Domain.Commands.Base;
using SmartCommandManager.Domain.Commands.Models;
using System.Text;

namespace SmartCommandManager.Modules.Core.Commands.HelpCommand
{
    public class HelpCommand : BaseCommand<Unit>
    {
        public override CommandInfo CommandInfo { get; } = new("help", "Help for application.", CommandCategory.Core);

        private readonly IReadOnlyList<ICommand> _commands;

        public HelpCommand(IReadOnlyList<ICommand> commands)
        {
            _commands = commands;
        }

        protected override CommandResult Execute(Unit args)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("--help: \n");
            foreach (var item in _commands) {
                sb.Append($"{item.CommandInfo.Name} - {item.CommandInfo.Description}" + "\n");
            }
            return new CommandResult() { Status = CommandStatus.Success, Message = sb.ToString() };
        }
    }
}
