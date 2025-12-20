namespace SmartCommandManager.Modules.FileSystem.Services
{ 
    public class FileService : IFileService
    {
        public void CreateFile(string sourceFile)
        {
            try {
                using (var fs = File.Create(sourceFile)) {
                    // File is created!
                }
            }
            catch (Exception ex) {
                throw new InvalidOperationException($"Cannot create file '{sourceFile}': {ex.Message}", ex);
            }
        }

        public void DeleteFile(string sourceFile)
        {
            try {
                File.Delete(sourceFile);
            }
            catch (Exception ex) {
                throw new InvalidOperationException($"Cannot delete file '{sourceFile}': {ex.Message}", ex);
            }
        }

        public void CopyFile(string sourceFile, string destinationFile, bool overwrite)
        {
            try {
                File.Copy(sourceFile, destinationFile, overwrite);
            }
            catch (Exception ex) {
                throw new InvalidOperationException($"Cannot copy file '{sourceFile}' to '{destinationFile}': {ex.Message}", ex);
            }
        }

        public void MoveFile(string sourceFile, string destinationFile, bool overwrite)
        {
            try {
                //if(!IsFile(destination)) {
                //    destination += "/" + source;
                //}
                File.Move(sourceFile, destinationFile, overwrite);
            }
            catch (Exception ex) {
                throw new InvalidOperationException($"Cannot move file '{sourceFile}' to '{destinationFile}': {ex.Message}", ex);
            }
        }

        public FileInfo GetFileInfo(string sourceFile)
        {
            try {
                return new FileInfo(sourceFile);
            }
            catch (Exception ex) {
                throw new InvalidOperationException($"Cannot get info for file '{sourceFile}': {ex.Message}", ex);
            }
        }

        public bool IsFile(string source)
        {
            return File.Exists(source);
        }
    }
}
