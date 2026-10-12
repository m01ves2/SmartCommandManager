namespace SmartCommandManager.Domain.Commands.Models
{
    public sealed record CommandHelpInfo(CommandInfo Info, IReadOnlyList<string> Aliases);
}
