namespace SmartCommandManager.Domain.Commands.Models
{
    public sealed record FlagDescriptor(string Canonical, IReadOnlyList<string> Aliases, string Description);
}
