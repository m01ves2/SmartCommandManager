using SmartCommandManager.Domain.NLP;

namespace SmartCommandManager.Domain.Commands
{
    public interface ICommand
    {
        CommandInfo CommandInfo { get; }

        IntentPattern IntentPattern { get; }
        CommandResult Execute(object args); // Универсальный вызов
    }
}
