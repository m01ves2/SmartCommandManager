using SmartCommandManager.Domain.Commands.Models;
using SmartCommandManager.Modules.FileSystem.Services;
using System.Text;

namespace SmartCommandManager.Modules.FileSystem.Commands.ListCommand
{
    public sealed class ListCommand : FileSystemCommandBase<ListArgs>
    {
        public override CommandInfo CommandInfo { get; } = new( "list",
                                                                "Show files and directories.",
                                                                CommandCategory.FileSystem,
                                                                usage: "list [<path>] [flags]",
                                                                details: """
                                                                    If path is omitted, use the current directory.
                                                                    Optional path markers: in, inside.
                                                                    Quote paths containing spaces.
                                                                    Wildcards are not supported.

                                                                    Examples:
                                                                      ls
                                                                      ls --long
                                                                      list in "docs"
                                                                    """,
                                                                flags: ListCommandFlags.All);

        public ListCommand(IFileSystemService fs, CommandContext context) : base(fs, context)
        {
        }

        public override CommandResult Execute(ListArgs args)
        {
            var source = PathNormalize(args.SourcePath);
            CommandResult commandResult;

            if (args.ListMode == ListMode.Directory) {
                commandResult = _fs.ListDirectory(source, args.LongListing, args.DirectoryOnly);
            }
            else if(args.ListMode == ListMode.File) {
                commandResult = _fs.ListFile(source, args.LongListing);//GetFileInfo(source);
            }
            else 
                commandResult = new CommandResult() { Status = CommandStatus.NotFound, Message = "No such file or directory" };
            return commandResult;
        }
    }
}
       
    
