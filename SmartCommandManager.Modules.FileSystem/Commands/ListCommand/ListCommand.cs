using SmartCommandManager.Domain.Commands.Models;
using SmartCommandManager.Modules.FileSystem.Services;
using System.Text;

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
            var source = PathNormalize(args.SourcePath);
            CommandResult commandResult;

            if (args.ListMode == ListMode.Directory) {
                commandResult = _fs.ListDirectory(source, args.LongListing, args.DirectoryOnly);
            }
            else if(args.ListMode == ListMode.File) {
                commandResult = _fs.ListFile(args.SourcePath);//GetFileInfo(source);
            }
            else 
                commandResult = new CommandResult() { Status = CommandStatus.NotFound, Message = "No such file or directory" };
            return commandResult;
        }
    }
}
       
    
