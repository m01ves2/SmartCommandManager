namespace SmartCommandManager.Modules.FileSystem.Commands.ListCommand.Builder
{
    public class FlagsCandidate
    {
        public bool LongListing { get; set; }
        public bool DirectoryOnly { get; set; }

        public FlagsCandidate(bool longListing, bool directoryOnly)
        {
            LongListing = longListing;
            DirectoryOnly = directoryOnly;
        }
    }
}
