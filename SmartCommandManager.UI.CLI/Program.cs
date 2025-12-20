using SmartCommandManager.Application.Services;
using SmartCommandManager.Domain.Commands.Models;
using SmartCommandManager.Modules.Core.Commands.ExitCommand;
using SmartCommandManager.Modules.Core.Commands.ExitCommand.NLP.Parsers;
using SmartCommandManager.Modules.Core.Commands.HelpCommand;
using SmartCommandManager.Modules.Core.Commands.HelpCommand.NLP.Parsers;
using SmartCommandManager.Modules.FileSystem.Commands.CopyCommand;
using SmartCommandManager.Modules.FileSystem.Commands.CopyCommand.NLP.Parsers;
using SmartCommandManager.Modules.FileSystem.Commands.ListCommand;
using SmartCommandManager.Modules.FileSystem.Commands.ListCommand.NLP.Parsers;
using SmartCommandManager.Modules.FileSystem.Services;
using SmartCommandManager.Modules.Thread.Commands.PsCommand;
using SmartCommandManager.Modules.Thread.Commands.PsCommand.Parsers;
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

            //3.Commands
            //3.1.Command Copy
            var copyCommand = new CopyCommand(fileSystemService, context);
            var copyArgsParser = new CopyArgsParser(fileSystemService);
            //3.2.Command Exit
            var exitCommand = new ExitCommand();
            var exitArgsParser = new ExitArgsParser();

            //3.3.Command Help
            var helpCommand = new HelpCommand(() => registry.AllCommandsInfo());
            var helpArgsParser = new HelpArgsParser();

            //3.4.Command Ps
            var psCommand = new PsCommand();
            var psArgsParser = new PsArgsParser();

            //3.5.Command List
            var listCommand = new ListCommand(fileSystemService, context);
            var listArgsParser = new ListArgsParser(fileSystemService);

            //4.Pipeline
            var copyIntentDescriptor = CopyIntentDescriptor.Descriptor;
            var copyPipeline = new CommandPipeline<CopyArgs>(copyCommand, copyArgsParser);

            var exitIntentDescriptor = ExitIntentDescriptor.Descriptor;
            var exitPipeline = new CommandPipeline<Unit>(exitCommand, exitArgsParser);

            var helpIntentDescriptor = HelpIntentDescriptor.Descriptor;
            var helpPipeline = new CommandPipeline<Unit>(helpCommand, helpArgsParser);

            var psIntenDescriptor = PsIntentDescriptor.Descriptor;
            var psPipeline = new CommandPipeline<Unit>(psCommand, psArgsParser);
            
            var listIntentDescriptor = ListIntentDescriptor.Descriptor;
            var listPipeline = new CommandPipeline<ListArgs>(listCommand, listArgsParser);
            //Register commands/intents
            registry.Register(copyIntentDescriptor, copyPipeline);
            registry.Register(exitIntentDescriptor, exitPipeline);
            registry.Register(psIntenDescriptor, psPipeline );
            registry.Register(listIntentDescriptor, listPipeline );
            registry.Register(helpIntentDescriptor, helpPipeline);


            //4.Dispatcher
            CommandDispatcher commandDispatcher = new CommandDispatcher(registry, tokenizer, intentParser, null);

            //5.UI
            var ui = new ConsoleUI(commandDispatcher, context);
            ui.Run();
        }
    }
}