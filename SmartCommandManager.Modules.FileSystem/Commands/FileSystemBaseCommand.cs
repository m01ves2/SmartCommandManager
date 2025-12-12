using SmartCommandManager.Domain.Commands;
using SmartCommandManager.Modules.FileSystem.Services;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.Marshalling;

namespace SmartCommandManager.Modules.FileSystem.Commands
{
    public abstract class FileSystemCommandBase<TArgs> : BaseCommand<TArgs>
    {
        protected readonly IFileSystemService _fs;
        protected readonly CommandContext _context;

        protected FileSystemCommandBase(IFileSystemService fs, CommandContext context)
        {
            _fs = fs;
            _context = context;
        }

        protected bool IsFile(string path) => File.Exists(path);
        protected bool IsDirectory(string path) => Directory.Exists(path);
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
    }
}

//using SmartCommandManager.Domain.Commands;
//using SmartCommandManager.Domain.NLP;
//using SmartCommandManager.Modules.FileSystem.Services;

//namespace SmartCommandManager.Modules.FileSystem.Commands
//{
//    public abstract class BaseFileSystemCommand : ITemplateCommand
//    {
//        //public abstract string Name { get; }
//        //public abstract string Description { get; }
//        //public virtual bool HideFromHelp => false;
//        //public abstract IEnumerable<string> NLPatterns { get; }

//        public abstract CommandInfo CommandInfo { get; }
//        public abstract IntentPattern IntentPattern { get; }

//        protected readonly IFileSystemService _fs;
//        protected readonly CommandContext _context;

//        public BaseFileSystemCommand(IFileSystemService fs, CommandContext context)
//        {
//            _fs = fs;
//            _context = context;
//        }

//        public abstract CommandResult Execute(CommandContext context);

//        protected bool IsFile(string path) => File.Exists(path);
//        protected bool IsDirectory(string path) => Directory.Exists(path);

//        protected (IEnumerable<string> flags, string source, string destination) ParseCommandArguments(string[] args) //TODO
//        {
//            var positionalArgs = args.Where(t => !t.StartsWith('-')).ToArray();
//            string source = positionalArgs.ElementAtOrDefault(0) ?? "";
//            string destination = positionalArgs.ElementAtOrDefault(1) ?? "";

//            var flags = args.Where(t => t.StartsWith('-')).SelectMany(t => t.Skip(1).Select(c => c.ToString()));

//            return (flags, source, destination);
//        }

//        protected string PathNormalize(string path)
//        {
//            if (string.IsNullOrWhiteSpace(path))
//                return string.Empty;

//            // Если путь абсолютный — возвращаем как есть
//            if (Path.IsPathRooted(path))
//                return Path.GetFullPath(path);

//            // Если путь относительный — комбинируем с текущей директорией
//            return Path.GetFullPath(Path.Combine(_context.CurrentDirectory, path));
//        }
//    }
//}
