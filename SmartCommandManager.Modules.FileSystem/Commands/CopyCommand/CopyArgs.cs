namespace SmartCommandManager.Modules.FileSystem.Commands.CopyCommand
{
    public enum CopyMode { 
        File,
        Directory,
        None
    };
    public class CopyArgs
    {
        public string SourcePath { get; set; }
        public string DestinationPath { get; set; }
        public bool Overwrite { get; set; }
        public bool Recursive { get; set; }
        public bool SkipIfExists { get; set; }
        public bool HasWildcard { get; set; }
        public CopyMode CopyMode { get; set; }
    }
}