using SmartCommandManager.Application.Services;
using SmartCommandManager.Domain.Commands.Models;
using SmartCommandManager.Modules.FileSystem.Commands.CopyCommand;
using SmartCommandManager.Modules.FileSystem.Commands.CopyCommand.NLP.Parsers;
using SmartCommandManager.Modules.FileSystem.Services;
using SmartCommandManager.NLP.Intent.Parsers;
using SmartCommandManager.NLP.Shared.Tokenizer;


namespace SmartCommandManager.UI.CLI
{
    public class Program
    {
        private static void Main(string[] args)
        {
            //1.UI state
            CommandContext context = new CommandContext();

            //2.Infrastructure
            var tokenizer = new Tokenizer();
            var registry = new CommandRegistry();
            var intentParser = new IntentParser();

            var fileService = new FileService();
            var directoryService = new DirectoryService();
            var fileSystemService = new FileSystemService(fileService, directoryService);

            //3.Command Copy
            var copyCommand = new CopyCommand(fileSystemService, context);
            var copyArgsParser = new CopyArgsParser();

            //4.Pipeline
            var copyIntentDescriptor = CopyIntentDescriptor.Descriptor;
            var copyPipeline = new CommandPipeline<CopyArgs>(copyCommand, copyArgsParser);

            //Register commands/intents
            registry.Register(copyIntentDescriptor, copyPipeline);

            //Dispatcher
            CommandDispatcher commandDispatcher = new CommandDispatcher(registry, tokenizer, intentParser, null);

            //UI
            var ui = new ConsoleUI(commandDispatcher, context);
            ui.Run();
        }
    }
}