using SmartCommandManager.Application.Services;
using SmartCommandManager.Domain.Commands.Models;

namespace SmartCommandManager.UI.CLI
{
    public class ConsoleUI
    {
        private readonly ICommandDispatcher _dispatcher;
        private readonly CommandContext _commandContext;
        private readonly TextCommandResultFormatter _formatter;
        public ConsoleUI(ICommandDispatcher dispatcher, CommandContext commandContext, TextCommandResultFormatter Formatter)
        {
            _dispatcher = dispatcher;
            _commandContext = commandContext;
            _formatter = Formatter;
        }

        public void Run()
        {
            Console.WriteLine("SmartCommandManager CLI");
            Console.WriteLine("Type 'help' to see available commands");

            while (true) {
                string prompt = _commandContext.CurrentDirectory + "> ";
                string input = ReadInput(prompt);

                CommandResult commandResult = _dispatcher.Execute(input);
                var output = _formatter.Format(commandResult);
                WriteOutput(output);
                //WriteOutput(commandResult);

                if (commandResult.Status == CommandStatus.Exit) {
                    break;
                }
            }
        }

        public void WriteOutput(FormattedOutput output)
        {
            switch (output.Style) {
                case OutputStyle.OK: WriteOK(output.Content); break;
                case OutputStyle.WARNING: WriteWarning(output.Content); break;
                case OutputStyle.ERROR: WriteError(output.Content); break;
                default: Write(output.Content); break;
            }
        }

        public string ReadInput(string prompt)
        {
            Write(prompt);
            //WriteOutput(prompt);
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
