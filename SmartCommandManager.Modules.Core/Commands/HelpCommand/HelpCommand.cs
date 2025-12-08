using SmartCommandManager.Domain.Commands;
using SmartCommandManager.Domain.NLP;
using System.Text;
using System.Windows.Input;

namespace SmartCommandManager.Modules.Core.Commands.HelpCommand
{
    public class HelpCommand : BaseCommand<Unit>
    {
        public override CommandInfo CommandInfo { get; } = new("help", "Help for application.", CommandCategory.Core);

        public override IntentPattern IntentPattern { get; } = new IntentPattern("exit", new[] { "help", "hlp", "man", "guide" });

        private readonly IReadOnlyList<Domain.Commands.ICommand> _commands;

        public HelpCommand(IReadOnlyList<Domain.Commands.ICommand> commands)
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
