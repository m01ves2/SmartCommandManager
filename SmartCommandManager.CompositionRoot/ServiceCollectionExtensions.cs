using SmartCommandManager.Application.Dispatcher;
using SmartCommandManager.Domain.Commands;
using SmartCommandManager.Modules.Core.Commands.ExitCommand;
using SmartCommandManager.Modules.Core.Commands.HelpCommand;
using SmartCommandManager.Modules.Core.Commands.UnknownCommand;
using SmartCommandManager.Modules.FileSystem.Commands.CopyCommand;
using SmartCommandManager.Modules.FileSystem.Commands.LicstCommand;
using SmartCommandManager.Modules.FileSystem.Services;a

namespace SmartCommandManager.CompositionRoot
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSmartFileManagerCore(this IServiceCollection services)
        {
            // Core services
            services.AddSingleton<IFileSystemService, FileSystemService>();
            services.AddSingleton<CommandContext>();
            services.AddSingleton<CommandRegistry>();
            services.AddSingleton<IDirectoryService, DirectoryService>();
            services.AddSingleton<IFileService, FileService>();

            // Commands
            RegisterCommands(services);

            //App services
            services.AddSingleton<CommandRegistry, CommandRegistry>();
            services.AddSingleton<CommandParser, CommandParser>();
            services.AddSingleton<ICommandDispatcher, CommandDispatcher>();
            services.AddSingleton<ICommandExecutor, CommandExecutor>();

            return services;
        }

        private static void RegisterCommands(IServiceCollection services)
        {
            services.AddSingleton<ITemplateCommand, CopyCommand>();
            services.AddSingleton<ITemplateCommand, CreateCommand>();
            services.AddSingleton<ITemplateCommand, DeleteCommand>();
            services.AddSingleton<ITemplateCommand, ListCommand>();
            services.AddSingleton<ITemplateCommand, MoveCommand>();
            services.AddSingleton<ITemplateCommand, ExitCommand>();
            services.AddSingleton<ITemplateCommand, CdCommand>();
            services.AddSingleton<ITemplateCommand, UnknownCommand>();
            services.AddSingleton<ITemplateCommand, HelpCommand>();
        }
    }
}
