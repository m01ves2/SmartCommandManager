using SmartCommandManager.Domain.Commands.Models;

namespace SmartCommandManager.Application.Services
{
    public interface ICommandDispatcher
    {
        CommandResult Execute(string input);
        string GetPrompt();
    }
}
