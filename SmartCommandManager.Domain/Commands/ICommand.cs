using SmartCommandManager.Domain.NLP;

namespace SmartCommandManager.Domain.Commands
{
    public interface ICommand
    {
        CommandInfo CommandInfo { get; }

        // Каждая команда предоставляет свои IntentPatterns
        IReadOnlyCollection<IntentPattern> IntentPatterns { get; }

        CommandResult Execute(CommandContext context);
    }
}
