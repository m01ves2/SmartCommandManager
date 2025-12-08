namespace SmartCommandManager.Modules.FileSystem.Commands.CopyCommand
{
    public class CopyArgs
    {
        public string Source { get; }
        public string Destination { get; }
        public IReadOnlyList<string> Flags { get; }


        public CopyArgs(string source, string destination, IReadOnlyList<string> flags)
        {
            Source = source;
            Destination = destination;
            Flags = flags;
        }
    }
}