namespace SmartCommandManager.Modules.FileSystem.Commands.CopyCommand.Builder
{
    public class FlagsCandidate
    {
        public bool Recursive { get; set; }
        public bool Overwrite { get; set; }  
        public bool SkipIfExists { get; set; }
        public bool HasWildcard { get; set; }

        public FlagsCandidate(bool recursive, bool overwrite, bool skipIfExists, bool hasWildcard) 
        { 
            Recursive = recursive;
            Overwrite = overwrite;
            SkipIfExists = skipIfExists;
            HasWildcard = hasWildcard;
        }
    }
}
