using SmartCommandManager.Domain.Commands;
using SmartCommandManager.Domain.NLP;
using SmartCommandManager.Modules.FileSystem.Services;

namespace SmartCommandManager.Modules.FileSystem.Commands.ListCommand
{
    public sealed class ListCommand : FileSystemCommandBase<ListArgs>
    {
        public override CommandInfo CommandInfo { get; } = new("list", "shows files and directories.", CommandCategory.FileSystem);

        public override IntentPattern IntentPattern { get; } = new IntentPattern("list", new[] { "list", "ls", "show", "reveal" });

        public ListCommand(IFileSystemService fs, CommandContext context) : base(fs, context)
        {
        }

        protected override CommandResult Execute(ListArgs args)
        {
            (IEnumerable<string> flags, string source) = (args.Flags, args.Source);

            if (string.IsNullOrWhiteSpace(source))
                source = ".";

            source = PathNormalize(source);

            CommandResult commandResult = _fs.List(flags, source);
            return commandResult;
        }
    }
}