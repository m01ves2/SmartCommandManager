using SmartCommandManager.Domain.Commands.Models;
using SmartCommandManager.Modules.FileSystem.Services;

namespace SmartCommandManager.Modules.FileSystem.Commands.ListCommand
{
    public sealed class ListCommand : FileSystemCommandBase<ListArgs>
    {
        public override CommandInfo CommandInfo { get; } = new("list", "shows files and directories.", CommandCategory.FileSystem);

        public ListCommand(IFileSystemService fs, CommandContext context) : base(fs, context)
        {
        }

        public override CommandResult Execute(ListArgs args)
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