using SmartCommandManager.Domain.NLP;

namespace SmartCommandManager.Domain.Commands
{
    public sealed class CommandContext
    {
        public string CurrentDirectory { get; set; } = Directory.GetCurrentDirectory();
        // TODO command history repository
    }
}
