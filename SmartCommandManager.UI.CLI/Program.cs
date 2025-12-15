using SmartCommandManager.NLP.Shared.Models;
using SmartCommandManager.NLP.Shared.Tokenizer;

namespace SmartCommandManager.UI.CLI
{
    public class Program
    {
        private static void Main(string[] args)
        {
            //var host = AppHost.Build(args, services =>
            //{
            //    services.AddSingleton<IUI, ConsoleUI>();
            //});

            //var ui = host.Provider.GetRequiredService<IUI>();

            // ConsoleUI has Run(), but IUI doesn't.
            //((ConsoleUI)ui).Run();

            string input = "please, copy 'all ' files \"from 'folder1' \" to folder2";

            Tokenizer tokenizer = new Tokenizer();
            IEnumerable<Token> tokens =  tokenizer.Tokenize(input);
            foreach (var item in tokens) {
                Console.WriteLine(item.Value);
            }

            // public CommandDispatcher(CommandContext commandContext, CommandRegistry commandRegistry, ITokenizer tokenizer, IIntentParser nlp, ILogger<CommandDispatcher> logger)
            //CommandContext commandContext = new CommandContext(tokens.ToList().AsReadOnly(), input);
            //CommandRegistry commandRegistry = new CommandRegistry();

            //CommandDispatcher commandDispatcher = new CommandDispatcher(tokenizer);
            Console.ReadKey();
        }
    }
}