using SmartCommandManager.Domain.Commands.Models;

namespace SmartCommandManager.Modules.FileSystem.Services
{
    public interface IFileSystemService
    {
        CommandResult CopyFile(string sourceFile, string destinationFile, bool overwrite);
        CommandResult CopyDirectory(string sourceDirectory, string destinationDirectory, bool recursive);

        CommandResult CreateFile(string source);
        CommandResult CreateDirectory(string source);

        CommandResult DeleteFile(string source);
        CommandResult DeleteDirectory(string source, bool recursive);

        CommandResult MoveFile(string source, string destination, bool overwrite);
        CommandResult MoveDirectory(string source, string destination);

        CommandResult ListFile(string source);
        CommandResult ListDirectory(string source, bool longListing, bool directoryOnly);
        ItemType GetItemType(string path);

    }
}
