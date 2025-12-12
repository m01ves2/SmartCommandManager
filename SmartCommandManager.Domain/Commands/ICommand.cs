namespace SmartCommandManager.Domain.Commands
{
    public interface ICommand
    {
        CommandInfo CommandInfo { get; }
        CommandResult Execute(object args); // Универсальный вызов
    }
}
