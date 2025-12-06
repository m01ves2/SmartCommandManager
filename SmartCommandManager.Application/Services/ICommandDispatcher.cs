using SmartCommandManager.Domain.Commands;

namespace SmartCommandManager.Application.Services
{
    public interface ICommandDispatcher
    {
        CommandResult Execute(string input);
        string GetPrompt();
    }
}
