using SmartCommandManager.Domain.NLP;

namespace SmartCommandManager.Domain.Commands
{
    public class CommandInfo
    {
        string Name { get; }
        string Description { get; }
        CommandCategory Category { get; }
    }
}
