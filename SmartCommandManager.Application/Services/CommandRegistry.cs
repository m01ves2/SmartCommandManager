using SmartCommandManager.Application.Exceptions;
using SmartCommandManager.Domain.Commands;

namespace SmartCommandManager.Application.Services
{
    /// <summary>
    /// Registry for Commands
    /// </summary>
    public class CommandRegistry
    {
        public IReadOnlyList<ICommand> Commands { get; }

        public CommandRegistry(IEnumerable<ICommand> commands)
        {
            Commands = commands.ToList();
        }

        public ICommand GetCommand(string intent)
        {
            ICommand? command = Commands.FirstOrDefault(c => c.IntentPattern.Synonyms.Contains(intent));

            if (command != null)
                return command;
            else {
                command = Commands.FirstOrDefault(c => c.IntentPattern.Primary.Equals("unknown"));
                if(command != null)
                    return command;
            }
            throw new CommandNotFoundException($"Command {intent} not registered.");
        }

        public IReadOnlyList<ICommand> GetCommands()
        {
            return Commands.ToList();
        }
    }
}