namespace SmartCommandManager.Modules.FileSystem.Commands.CopyCommand.Builder
{
    public class FlagsCandidate
    {
        bool Overwite { get; set; }  
        bool Recursive { get; set; }
        bool SkipIfExists { get; set; }
        bool HasWildcard { get; set; }
    }
}
