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

            CommandResult commandResult;
            if (args.CopyMode == CopyMode.File) {
                commandResult = _fs.CopyFile(args.SourcePath, args.DestinationPath, args.Overwrite);
            }
            else if (args.CopyMode == CopyMode.Directory) {
                commandResult = _fs.CopyDirectory(args.SourcePath, args.DestinationPath, args.Recursive);
            }
            else
                commandResult = new CommandResult() { Status = CommandStatus.Failed, Message = $"Operation failed. No such file or directory" };
            return commandResult;
        }
    }
}
