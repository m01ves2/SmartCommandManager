using SmartCommandManager.Application.Dispatcher;
using SmartCommandManager.Domain.Commands;
using SmartCommandManager.Modules.Core.Commands.ExitCommand;
using SmartCommandManager.Modules.Core.Commands.HelpCommand;
using SmartCommandManager.Modules.Core.Commands.UnknownCommand;
using SmartCommandManager.Modules.FileSystem.Commands.CopyCommand;
using SmartCommandManager.Modules.FileSystem.Commands.LicstCommand;
using SmartCommandManager.Modules.FileSystem.Services;
using SmartFileManager.App.Interfaces;

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
            services.AddSingleton<ICommand, CopyCommand>();
            services.AddSingleton<ICommand, CreateCommand>();
            services.AddSingleton<ICommand, DeleteCommand>();
            services.AddSingleton<ICommand, ListCommand>();
            services.AddSingleton<ICommand, MoveCommand>();
            services.AddSingleton<ICommand, ExitCommand>();
            services.AddSingleton<ICommand, CdCommand>();
            services.AddSingleton<ICommand, UnknownCommand>();
            services.AddSingleton<ICommand, HelpCommand>();
        }
    }
}
