namespace SmartCommandManager.Modules.FileSystem.Commands.ListCommand
{
    public class ListArgs
    {
        public string Source { get; }
        public IReadOnlyList<string> Flags { get; }

        public ListArgs (string? source, IReadOnlyList<string> flags)
        {
            Source = source;
            Flags = flags;
        }
    }
}
