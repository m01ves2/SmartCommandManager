using SmartCommandManager.Application.NLP;
using SmartCommandManager.Domain.NLP;

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
            Tokenizer tokenizer = new Tokenizer();
            IEnumerable<Token> tokens =  tokenizer.Tokenize("please, copy 'all ' files \"from 'folder1' \" to folder2");
            foreach (var item in tokens) {
                Console.WriteLine(item.Value);
            }
            Console.ReadKey();
        }
    }
}