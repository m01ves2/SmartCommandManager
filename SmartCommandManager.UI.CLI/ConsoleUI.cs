using SmartCommandManager.Application.Services;
using SmartCommandManager.Domain.Commands.Models;

namespace SmartCommandManager.UI.CLI
{
    public class ConsoleUI : IUI
    {
        private readonly ICommandDispatcher _dispatcher;
        private readonly CommandContext _commandContext;
        public ConsoleUI(ICommandDispatcher dispatcher, CommandContext commandContext)
        {
            _dispatcher = dispatcher;
            _commandContext = commandContext;
        }

        public void Run()
        {

            while (true) {
                string prompt = _commandContext.CurrentDirectory + "> ";
                string input = ReadInput(prompt);

                CommandResult commandResult = _dispatcher.Execute(input);
                WriteOutput(commandResult);

                if (commandResult.Status == CommandStatus.Exit) {
                    break;
                }
            }
        }

        public void WriteOutput(CommandResult commandResult)
        {
            if (commandResult.Message == null) {
                WriteError("Unknown result of command");
                return;
            }

            if (commandResult.Status == CommandStatus.Success)
                WriteOK(commandResult.Message);
            else if (commandResult.Status == CommandStatus.Failed)
                WriteError(commandResult.Message);
            else if (commandResult.Status == CommandStatus.Exit)
                WriteWarning(commandResult.Message);
            else
                WriteWarning($"Unhandled status: {commandResult.Status}");
        }

        public string ReadInput(string prompt)
        {
            Write(prompt);
            string input = (Console.ReadLine() ?? "").Trim();
            return input;
        }

        private void Write(string message)
        {
            Console.ResetColor();
            Console.Write(message);
        }
        private void WriteOK(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        private void WriteWarning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        private void WriteError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
