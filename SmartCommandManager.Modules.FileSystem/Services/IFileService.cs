namespace SmartCommandManager.Modules.FileSystem.Services
{
    // IFileService.cs — сервис для работы с файловой системой
    public interface IFileService
    {
        FileInfo GetFileInfo(string path);
        void CreateFile(string path);
        void DeleteFile(string path);
        void CopyFile(string source, string destination, bool overwrite);
        void MoveFile(string source, string destination, bool overwrite);

        bool IsFile(string source);
    }
}
