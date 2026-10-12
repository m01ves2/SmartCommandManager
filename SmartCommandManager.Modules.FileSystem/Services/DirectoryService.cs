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

        public string[] ListDirectory(string path, bool directoryOnly)
        {
            try {
                var entries = directoryOnly ? Directory.GetDirectories(path) : Directory.GetFileSystemEntries(path);

                return entries.Select(entry => Path.GetRelativePath(path, entry)).ToArray();
            }
            catch (Exception ex) {
                throw new InvalidOperationException($"Cannot list directory '{path}': {ex.Message}", ex);
            }
        }

        public void CopyDirectory(string source, string destination, bool recursive)
        {
            try {
                ValidateCopyPaths(source, destination);

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

        //защита копирования директории в свою же поддиректорию
        private static void ValidateCopyPaths(string source, string destination)
        {
            string sourcePath = Path.TrimEndingDirectorySeparator(Path.GetFullPath(source)); // приводим путь к абсолютному виду и обрабатываем . / ...

            string destinationPath = Path.TrimEndingDirectorySeparator(Path.GetFullPath(destination)); // Удаление завершающего разделителя позволяет одинаково сравнивать docs и docs/.

            StringComparison comparison = OperatingSystem.IsWindows() //Для Windows используем обычное сравнение без учёта регистра, для Linux — с учётом
                ? StringComparison.OrdinalIgnoreCase
                : StringComparison.Ordinal;

            string sourcePrefix = Path.EndsInDirectorySeparator(sourcePath)
                ? sourcePath
                : sourcePath + Path.DirectorySeparatorChar;//Разделитель в sourcePrefix отличает подкаталог docs/backup от соседнего каталога docs-old

            if (string.Equals(sourcePath, destinationPath, comparison)
                || destinationPath.StartsWith(sourcePrefix, comparison)) {
                throw new InvalidOperationException("Cannot copy a directory into itself or its subdirectory");
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

        public DirectoryInfo GetDirectoryInfo(string path)
        {
            try {
                return new DirectoryInfo(path);
            }
            catch (Exception ex) {
                throw new InvalidOperationException($"Cannot get info for directory '{path}': {ex.Message}", ex);
            }
        }
    }
}
