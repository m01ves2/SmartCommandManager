namespace SmartCommandManager.Modules.FileSystem.Commands.CopyCommand
{
    public class CopyArgs
    {
        public string SourcePath { get; init; }
        public string DestinationPath { get; init; }
        public bool Overwrite { get; init; }
        public bool Recursive { get; init; }
        public bool SkipIfExists { get; init; }
        public bool HasWildcard { get; init; }

    }
}