namespace SmartCommandManager.Modules.FileSystem.Commands.CopyCommand
{
    public class CopyCommand : BaseCommand
    {
        public override string Name => "cp";
        public override string Description => "Copy file or directory";
        public override IEnumerable<string> NLPatterns { get; } = ["cp", "copy", "duplicate", "clone"];
        public CopyCommand(IFileSystemService fs, CommandContext context) : base(fs, context)
        {
        }

        public override CommandResult Execute(string[] args)
        {

            (IEnumerable<string> flags, string source, string destination) = ParseCommandArguments(args);

            if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(destination))
                return new CommandResult { Status = CommandStatus.Error, Message = "source and destination paths required" };

            source = PathNormalize(source);
            destination = PathNormalize(destination);

            CommandResult commandResult = _fs.Copy(flags, source, destination);
            return commandResult;
        }
    }
}
