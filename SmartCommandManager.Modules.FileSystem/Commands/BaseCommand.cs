using SmartCommandManager.Application.Dispatcher;
using SmartCommandManager.Domain.NLP;
using SmartCommandManager.Modules.FileSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCommandManager.Modules.FileSystem.Commands
{
    public abstract class BaseCommand : ICommand
    {
        public abstract string Name { get; }
        public abstract string Description { get; }
        public virtual bool HideFromHelp => false;

        public abstract IEnumerable<string> NLPatterns { get; }

        protected readonly IFileSystemService _fs;
        protected readonly CommandContext _context;

        public BaseCommand(IFileSystemService fs, CommandContext context)
        {
            _fs = fs;
            _context = context;
        }

        protected bool IsFile(string path) => File.Exists(path);
        protected bool IsDirectory(string path) => Directory.Exists(path);

        protected (IEnumerable<string> flags, string source, string destination) ParseCommandArguments(string[] args)
        {
            var positionalArgs = args.Where(t => !t.StartsWith('-')).ToArray();
            string source = positionalArgs.ElementAtOrDefault(0) ?? "";
            string destination = positionalArgs.ElementAtOrDefault(1) ?? "";

            var flags = args.Where(t => t.StartsWith('-')).SelectMany(t => t.Skip(1).Select(c => c.ToString()));

            return (flags, source, destination);
        }

        //public abstract CommandResult Execute(string[] args);

        protected string PathNormalize(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return string.Empty;

            // Если путь абсолютный — возвращаем как есть
            if (Path.IsPathRooted(path))
                return Path.GetFullPath(path);

            // Если путь относительный — комбинируем с текущей директорией
            return Path.GetFullPath(Path.Combine(_context.CurrentDirectory, path));
        }

        public CommandResult Execute(IEnumerable<Token> argTokens)
        {
            throw new NotImplementedException();
        }
    }
}
