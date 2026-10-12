using SmartCommandManager.Domain.Commands.Models;
using SmartCommandManager.Modules.FileSystem.Services;

namespace SmartCommandManager.Modules.FileSystem.Commands.CopyCommand
{
    public sealed class CopyCommand : FileSystemCommandBase<CopyArgs>
    {
        public override CommandInfo CommandInfo { get; } = new( "copy",
                                                                "Copy files and directories.",
                                                                CommandCategory.FileSystem,
                                                                usage: "copy <source> <destination> [flags]",
                                                                details: """
                                                                    Optional source markers: from, in, inside.
                                                                    Optional destination markers: to, into.
                                                                    Quote paths containing spaces.
                                                                    Overwriting and skipping cannot be used together.
                                                                    Wildcards are not supported.

                                                                    Examples:
                                                                      cp "report.txt" "report-copy.txt"
                                                                      copy from "docs" to "docs-copy" --recursive
                                                                    """,
                                                                flags: CopyCommandFlags.All);

        public CopyCommand(IFileSystemService fs, CommandContext context) : base(fs, context)
        {
        }

        public override CommandResult Execute(CopyArgs args)
        {
            var source = PathNormalize(args.SourcePath);
            var destination = PathNormalize(args.DestinationPath);

            if (args.SkipIfExists  && _fs.GetItemType(destination) != ItemType.NONE) {
                return new CommandResult(){ Status = CommandStatus.Success, Message = $"Skipped: destination '{destination}' already exists" };
            }

            CommandResult commandResult;
            if (args.CopyMode == CopyMode.File) {
                commandResult = _fs.CopyFile(source, destination, args.Overwrite);
            }
            else if (args.CopyMode == CopyMode.Directory) {
                commandResult = _fs.CopyDirectory(source, destination, args.Recursive);
            }
            else
                commandResult = new CommandResult() { Status = CommandStatus.Failed, Message = $"Operation failed. No such file or directory" };

            return commandResult;
        }
    }
}
