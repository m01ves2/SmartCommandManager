using SmartCommandManager.Domain.Commands.Base;
using SmartCommandManager.Domain.Commands.Models;
using System.Text;

namespace SmartCommandManager.Modules.Core.Commands.HelpCommand
{
    public class HelpCommand : BaseCommand<Unit>
    {
        public override CommandInfo CommandInfo { get; } = new("help", "Help for application.", CommandCategory.Core);

        private readonly Func<IEnumerable<CommandHelpInfo>> _getCommands;

        public HelpCommand(Func<IEnumerable<CommandHelpInfo>> getCommands)
        {
            _getCommands = getCommands;
        }

        public override CommandResult Execute(Unit args)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Available commands:");
            sb.AppendLine();

            foreach (var entry in _getCommands()) {
                CommandInfo info = entry.Info;

                sb.AppendLine($"{info.Name} - {info.Description}");

                if (entry.Aliases.Count > 0) {
                    sb.AppendLine(
                        $"  Aliases: {string.Join(", ", entry.Aliases)}");
                }

                if (!string.IsNullOrWhiteSpace(info.Usage)) {
                    sb.AppendLine($"  Usage: {info.Usage}");
                }

                if (info.Flags.Count > 0) {
                    sb.AppendLine("  Flags:");

                    foreach (var flag in info.Flags) {
                        sb.AppendLine(
                            $"    {string.Join(", ", flag.Aliases)} — {flag.Description}");
                    }
                }

                if (!string.IsNullOrWhiteSpace(info.Details)) {
                    sb.AppendLine(info.Details);
                }

                sb.Append("----------");
                sb.AppendLine();
                sb.AppendLine();
            }

            return new CommandResult
            {
                Status = CommandStatus.Success,
                Message = sb.ToString()
            };
        }
    }
}
