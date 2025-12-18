using SmartCommandManager.Domain.Commands.Models;

namespace SmartCommandManager.Modules.FileSystem.Services
{
    public interface IFileSystemService
    {
        CommandResult CopyFile(string sourceFile, string destinationFile, bool overwrite);
        CommandResult CopyDirectory(string sourceDirectory, string destinationDirectory, bool recursive);


        CommandResult Delete(string source, bool recursive);
        CommandResult Create(IEnumerable<string> flags, string source);
        CommandResult Move(string source, string destination, bool overwrite);
        CommandResult List(IEnumerable<string> flags, string source);
        ItemType GetItemType(string path);

    }
}
