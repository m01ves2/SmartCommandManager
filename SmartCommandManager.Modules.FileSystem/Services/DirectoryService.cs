//low-level working with files/directories
namespace SmartCommandManager.Modules.FileSystem.Services
{
    public class DirectoryService : IDirectoryService
    {
        public void CreateDirectory(string sourceDirectory)
        {
            try {
                Directory.CreateDirectory(sourceDirectory);
            }
            catch (Exception ex) {
                throw new InvalidOperationException($"Cannot create directory '{sourceDirectory}': {ex.Message}", ex);
            }
        }

        public void DeleteDirectory(string path, bool recursive)
        {
            try {
                Directory.Delete(path, recursive);
            }
            catch (Exception ex) {
                throw new InvalidOperationException($"Cannot delete directory '{path}': {ex.Message}", ex);
            }
        }

        public string[] ListDirectory(string path)
        {
            try {
                var entries = Directory.GetFileSystemEntries(path);
                // Преобразуем абсолютные пути в относительные
                return entries.Select(entry => Path.GetRelativePath(path, entry)).ToArray();
            }
            catch (Exception ex) {
                throw new InvalidOperationException($"Cannot list directory '{path}': {ex.Message}", ex);
            }
        }

        public void CopyDirectory(string source, string destination, bool recursive)
        {
            try {
                Directory.CreateDirectory(destination);
                foreach (var file in Directory.GetFiles(source))
                    File.Copy(file, Path.Combine(destination, Path.GetFileName(file)), true);
                if (recursive) {
                    foreach (var dir in Directory.GetDirectories(source))
                        CopyDirectory(dir, Path.Combine(destination, Path.GetFileName(dir)), recursive);
                }
            }
            catch (Exception ex) {
                throw new InvalidOperationException($"Cannot copy directory '{source}' to '{destination}': {ex.Message}", ex);
            }
        }

        public void MoveDirectory(string source, string destination)
        {
            try {
                Directory.Move(source, destination);
            }
            catch (Exception ex) {
                throw new InvalidOperationException($"Cannot move directory '{source}' to '{destination}': {ex.Message}", ex);
            }
        }

        public bool IsDirectory(string source)
        {
            return Directory.Exists(source); 
        }
    }
}
