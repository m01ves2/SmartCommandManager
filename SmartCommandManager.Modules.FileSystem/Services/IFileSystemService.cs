namespace SmartCommandManager.Modules.FileSystem.Services
{
    public interface IFileSystemService
    {
        CommandResult Copy(IEnumerable<string> flags, string source, string destination);
        CommandResult Delete(IEnumerable<string> flags, string source);
        CommandResult Create(IEnumerable<string> flags, string source);
        CommandResult Move(IEnumerable<string> flags, string source, string destination);
        CommandResult List(IEnumerable<string> flags, string source);

    }
}
