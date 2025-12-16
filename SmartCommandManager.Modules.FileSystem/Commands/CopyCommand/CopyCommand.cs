using SmartCommandManager.Domain.Commands.Models;
using SmartCommandManager.Modules.FileSystem.Services;

namespace SmartCommandManager.Modules.FileSystem.Commands.CopyCommand
{
    public sealed class CopyCommand : FileSystemCommandBase<CopyArgs>
    {
        public override CommandInfo CommandInfo { get; } = new("copy", "Copy files and directories.", CommandCategory.FileSystem);

        public CopyCommand(IFileSystemService fs, CommandContext context) : base(fs, context)
        {
        }

        public override CommandResult Execute(CopyArgs args)
        {
            var source = PathNormalize(args.SourcePath);
            var destination = PathNormalize(args.DestinationPath);

            CommandResult commandResult = _fs.Copy(args.SourcePath, args.DestinationPath, args.Recursive, args.Overwrite);
            return commandResult;
        }
    }
}
