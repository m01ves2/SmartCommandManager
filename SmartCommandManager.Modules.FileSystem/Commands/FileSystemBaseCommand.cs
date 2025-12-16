using SmartCommandManager.Domain.Commands.Base;
using SmartCommandManager.Domain.Commands.Models;
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