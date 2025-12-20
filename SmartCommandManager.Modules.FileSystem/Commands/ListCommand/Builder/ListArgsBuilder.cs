using SmartCommandManager.Modules.FileSystem.Commands.ListCommand;
using SmartCommandManager.Modules.FileSystem.Commands.ListCommand.Builder;
using SmartCommandManager.Modules.FileSystem.Commands.ListCommand.NLP.Models;
using SmartCommandManager.Modules.FileSystem.Commands.ListCommand.NLP.Validators;
using SmartCommandManager.Modules.FileSystem.Services;

namespace SmartCommandManager.Modules.FileSystem.Commands.ListCommand.Builder
{
    public class ListArgsBuilder
    {
        private readonly IFileSystemService _fs;

        public ListArgsBuilder(IFileSystemService fs)
        {
            _fs = fs;
        }

        public ListArgs Build(ListParseTree tree)
        {
            SourceCandidate sourceCandidate = BuildSource(tree);
            FlagsCandidate flags = BuildFlags(tree);
            return BuildListArgs(sourceCandidate, flags);
        }
        private SourceCandidate BuildSource(ListParseTree tree)
        {
            SourceSyntaxValidator.Validate(tree);
            string path = ResolveSourcePath(tree);
            SourceCandidate candidate = BuildSourceCandidate(path);
            SourceSemanticsValidator.Validate(candidate);
            return candidate;
        }

        private string ResolveSourcePath(ListParseTree tree)
        {
            return tree.SourcePaths.Paths.Single();
        }

        private SourceCandidate BuildSourceCandidate(string path)
        {
            ItemType sourceType = _fs.GetItemType(path);
            bool exists = sourceType != ItemType.NONE;

            return new SourceCandidate(path, sourceType, exists);
        }

        private FlagsCandidate BuildFlags(ListParseTree tree)
        {
            bool longListing = tree.Flags.Flags.Contains("longlisting");
            bool directoryOnly = tree.Flags.Flags.Contains("directoryonly");

            return new FlagsCandidate(longListing, directoryOnly);
        }
        private ListArgs BuildListArgs(SourceCandidate sourceCandidate, FlagsCandidate flags)
        {
            return new ListArgs()
            {
                ListMode = (sourceCandidate.ItemType == ItemType.DIRECTORY) ? ListMode.Directory : ListMode.File,
                SourcePath = sourceCandidate.Path,
                LongListing = flags.LongListing,
                DirectoryOnly = flags.DirectoryOnly,
            };
        }
    }
}
