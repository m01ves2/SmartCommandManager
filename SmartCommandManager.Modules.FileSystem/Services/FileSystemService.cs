using SmartCommandManager.Domain.Commands.Models;
using System.Text;

namespace SmartCommandManager.Modules.FileSystem.Services
{
    public enum ItemType
    {
        NONE,
        FILE,
        DIRECTORY,
    };

    public class FileSystemService : IFileSystemService
    {
        private readonly IFileService _fileService;
        private readonly IDirectoryService _directoryService;

        public FileSystemService(IFileService fileService, IDirectoryService directoryService)
        {
            _fileService = fileService;
            _directoryService = directoryService;
        }

        public CommandResult CopyFile(string sourceFile, string destinationFile, bool overwrite)
        {
            _fileService.CopyFile(sourceFile, destinationFile, overwrite);
            return new CommandResult { Status = CommandStatus.Success, Message = $"File ${sourceFile} copied to {destinationFile}" };
        }

        public CommandResult CopyDirectory(string sourceDirectory, string destinationDirectory, bool recursive)
        {
            _directoryService.CopyDirectory(sourceDirectory, destinationDirectory, recursive);
            return new CommandResult { Status = CommandStatus.Success, Message = $"Directory ${sourceDirectory} copied to {destinationDirectory}" };
        }

        public CommandResult CreateFile(string source)
        {
            // if (flags.Count() == 0 || !(flags.Contains("d"))) type = ItemType.FILE;
            _fileService.CreateFile(source);
            return new CommandResult { Status = CommandStatus.Success, Message = $"Created file {source}" };
        }

        public CommandResult CreateDirectory(string source)
        {
            _directoryService.CreateDirectory(source);
            return new CommandResult { Status = CommandStatus.Success, Message = $"Created directory {source}" };
        }

        public CommandResult DeleteFile(string source)
        {
            _fileService.DeleteFile(source);
            return new CommandResult { Status = CommandStatus.Success, Message = $"Deleted file {source}" };
        }
        public CommandResult DeleteDirectory(string source, bool recursive)
        {
            _directoryService.DeleteDirectory(source, recursive);
            return new CommandResult { Status = CommandStatus.Success, Message = $"Deleted directory {source}" };
        }

        public CommandResult MoveFile(string source, string destination, bool overwrite) //TODO
        {
            _fileService.MoveFile(source, destination, overwrite);
            return new CommandResult { Status = CommandStatus.Success, Message = $"File moved to {destination}" };

        }

        public CommandResult MoveDirectory(string source, string destination)
        {
            _directoryService.MoveDirectory(source, destination);
            return new CommandResult { Status = CommandStatus.Success, Message = $"Directory moved to {destination}" };
        }

        public CommandResult ListFile(string source)
        {
            FileInfo fileInfo = _fileService.GetFileInfo(source);
            return new CommandResult()
            {
                Status = CommandStatus.Success,
                Message = $"{fileInfo.CreationTime}\t{fileInfo.Attributes}\t{fileInfo.Length}"
            };
        }

        public CommandResult ListDirectory(string source, bool longListing, bool directoryOnly)
        {
            string[] items = _directoryService.ListDirectory(source);
            StringBuilder sb = new StringBuilder();
            foreach (string i in items) {
                sb.Append(i + "\n");
            }
            return new CommandResult() { Status = CommandStatus.Success, Message = sb.ToString() };
        }

        public ItemType GetItemType(string path)
        {
            ItemType type;
            if (_fileService.IsFile(path))
                type = ItemType.FILE;
            else if (_directoryService.IsDirectory(path))
                type = ItemType.DIRECTORY;
            else
                type = ItemType.NONE;
            return type;

        }
    }
}
