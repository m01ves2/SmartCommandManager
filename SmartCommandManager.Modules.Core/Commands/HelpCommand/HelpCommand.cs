using System.Text;
using System.Windows.Input;

namespace SmartCommandManager.Modules.Core.Commands.HelpCommand
{
    public class HelpCommand : BaseCommand, ICommandsAware
    {
        public override string Name => "help";
        public override string Description => "Displays a list of available commands.";
        public override IEnumerable<string> NLPatterns { get; } = ["help", "man", "manual", "guide", "hlp"];

        private IReadOnlyList<ICommand> _commands = [];

        public HelpCommand(IFileSystemService fs, CommandContext commandContext) : base(fs, commandContext)
        {
        }

        public override CommandResult Execute(string[] args)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("--help: \n");
            foreach (var item in _commands.Where(c => c.HideFromHelp == false)) {
                sb.Append($"{item.Name} - {item.Description}" + "\n");
            }
            return new CommandResult() { Status = CommandStatus.Success, Message = sb.ToString() };
        }

        public void SetCommands(IReadOnlyList<ICommand> commands)
        {
            _commands = commands;
        }
    }
}
