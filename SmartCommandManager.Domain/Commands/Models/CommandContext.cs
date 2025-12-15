namespace SmartCommandManager.Domain.Commands.Models
{
    public sealed class CommandContext
    {
        public string CurrentDirectory { get; set; } = Directory.GetCurrentDirectory();
        // TODO command history repository
    }
}
