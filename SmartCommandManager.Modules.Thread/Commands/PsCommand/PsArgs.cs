namespace SmartCommandManager.Modules.Thread.Commands.PsCommand
{
    public class PsArgs
    {
        public IReadOnlyList<string> Flags { get; }
        public PsArgs( IReadOnlyList<string> flags)
        {

            Flags = flags;
        }
    }
}
