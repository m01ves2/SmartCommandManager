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

namespace SmartCommandManager.CompositionRoot
{
    public static class CompositionRoot
    {
        public static ApplicationGraph Build()
        {
            // 1. UI state
            CommandContext context = new CommandContext();

            // 2. Infrastructure
            var tokenizer = new Tokenizer();
            var registry = new CommandRegistry();
            var intentParser = new IntentParser();

            var fileService = new FileService();
            var directoryService = new DirectoryService();
            var fileSystemService = new FileSystemService( fileService, directoryService);

            // 3. Commands
            var copyCommand = new CopyCommand(fileSystemService, context);
            var copyArgsParser = new CopyArgsParser(fileSystemService);

            var exitCommand = new ExitCommand();
            var exitArgsParser = new ExitArgsParser();

            var helpCommand = new HelpCommand(() => registry.AllCommandsInfo());
            var helpArgsParser = new HelpArgsParser();

            var psCommand = new PsCommand();
            var psArgsParser = new PsArgsParser();

            var listCommand = new ListCommand(fileSystemService, context);
            var listArgsParser = new ListArgsParser(fileSystemService);

            // 4. Pipelines
            registry.Register(
                CopyIntentDescriptor.Descriptor,
                new CommandPipeline<CopyArgs>(copyCommand, copyArgsParser));

            registry.Register(
                ExitIntentDescriptor.Descriptor,
                new CommandPipeline<Unit>(exitCommand, exitArgsParser));

            registry.Register(
                HelpIntentDescriptor.Descriptor,
                new CommandPipeline<Unit>(helpCommand, helpArgsParser));

            registry.Register(
                PsIntentDescriptor.Descriptor,
                new CommandPipeline<Unit>(psCommand, psArgsParser));

            registry.Register(
                ListIntentDescriptor.Descriptor,
                new CommandPipeline<ListArgs>(listCommand, listArgsParser));

            // 5. Dispatcher
            var dispatcher = new CommandDispatcher(
                registry,
                tokenizer,
                intentParser,
                null);

            // 6. Return application graph
            return new ApplicationGraph(dispatcher, context);
        }
    }
}
