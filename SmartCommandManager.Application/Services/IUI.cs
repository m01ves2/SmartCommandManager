using SmartCommandManager.Domain.Commands.Models;

namespace SmartCommandManager.Application.Services
{
    public interface IUI
    {
        public string ReadInput(string prompt);
        public void WriteOutput(CommandResult commandResult);
    }
}
