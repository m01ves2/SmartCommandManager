using SmartCommandManager.Domain.Commands.Models;

namespace SmartCommandManager.Domain.Commands.Base
{
    public interface ICommand
    {
        CommandInfo CommandInfo { get; }
        CommandResult Execute(object args); // Универсальный вызов
    }
}
