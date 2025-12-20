namespace SmartCommandManager.Modules.FileSystem.Commands.ListCommand
{
    public enum ListMode
    {
        File,
        Directory,
        None
    };
    public class ListArgs
    {
        public string SourcePath { get; set; }
        public bool DirectoryOnly { get; set; }
        public bool LongListing { get; set; }
        public ListMode ListMode { get; set; }
    }
}
