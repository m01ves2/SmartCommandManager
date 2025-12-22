using Microsoft.Extensions.DependencyInjection;
using SmartCommandManager.Application.Services;
using SmartCommandManager.Domain.Commands.Base;
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
using SmartCommandManager.NLP.Command.Parsers;
using SmartCommandManager.NLP.Intent.Parsers;
using SmartCommandManager.NLP.Shared.Tokenizer;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;


namespace SmartCommandManager.CompositionRoot
{
    public static class CompositionRoot
    {

        public static ApplicationRoot Build()
        {
            var services = new ServiceCollection();

            RegisterServices(services);

            var provider = services.BuildServiceProvider();

            // ВАЖНО: наполняем registry ПОСЛЕ сборки контейнера
            RegisterPipelines( provider.GetRequiredService<CommandRegistry>(), provider);

            return provider.GetRequiredService<ApplicationRoot>();
        }

        private static void RegisterServices(IServiceCollection services)
        {
            // Application root
            services.AddSingleton<ApplicationRoot>();

            // UI state
            services.AddSingleton<CommandContext>();

            // Infrastructure
            services.AddSingleton<ITokenizer, Tokenizer>();
            services.AddSingleton<IIntentParser, IntentParser>();
            services.AddSingleton<CommandRegistry>();

            services.AddSingleton<IFileService, FileService>();
            services.AddSingleton<IDirectoryService, DirectoryService>();
            services.AddSingleton<IFileSystemService, FileSystemService>();

            //Logging
            services.AddSingleton<ILogger>(NullLogger.Instance);

            //Регистрируем команды
            //-поведение
            //-без состояния
            //-не кэшируется
            //=> AddTransient
            services.AddTransient<CopyCommand>();
            services.AddTransient<ListCommand>();
            services.AddTransient<ExitCommand>();
            services.AddTransient<PsCommand>();
            // HelpCommand — ЛЕНИВАЯ зависимость
            services.AddTransient<HelpCommand>(sp => new HelpCommand(() => sp.GetRequiredService<CommandRegistry>().AllCommandsInfo()));



            //parsers
            services.AddSingleton<IArgsParser<CopyArgs>, CopyArgsParser>();
            services.AddSingleton<IArgsParser<ListArgs>, ListArgsParser>();
            services.AddSingleton<IArgsParser<Unit>, ExitArgsParser>();
            services.AddSingleton<IArgsParser<Unit>, PsArgsParser>();
            services.AddSingleton<IArgsParser<Unit>, HelpArgsParser>();

            // Dispatcher
            services.AddSingleton<CommandDispatcher>();
        }

        private static void RegisterPipelines( CommandRegistry registry, IServiceProvider provider)
        {
            // Copy
            registry.Register(
                CopyIntentDescriptor.Descriptor,
                new CommandPipeline<CopyArgs>(
                    provider.GetRequiredService<CopyCommand>(),
                    provider.GetRequiredService<IArgsParser<CopyArgs>>()
                )
            );

            // List
            registry.Register(
                ListIntentDescriptor.Descriptor,
                new CommandPipeline<ListArgs>(
                    provider.GetRequiredService<ListCommand>(),
                    provider.GetRequiredService<IArgsParser<ListArgs>>()
                )
            );

            // Ps
            registry.Register(
                PsIntentDescriptor.Descriptor,
                new CommandPipeline<Unit>(
                    provider.GetRequiredService<PsCommand>(),
                    provider.GetRequiredService<IArgsParser<Unit>>()
                )
            );

            // Exit
            registry.Register(
                ExitIntentDescriptor.Descriptor,
                new CommandPipeline<Unit>(
                    provider.GetRequiredService<ExitCommand>(),
                    provider.GetRequiredService<IArgsParser<Unit>>()
                )
            );

            // Help — БЕЗ ЦИКЛА
            registry.Register(
                HelpIntentDescriptor.Descriptor,
                new CommandPipeline<Unit>(
                    provider.GetRequiredService<HelpCommand>(),
                    provider.GetRequiredService<IArgsParser<Unit>>()
                )
            );
        }
    }
}