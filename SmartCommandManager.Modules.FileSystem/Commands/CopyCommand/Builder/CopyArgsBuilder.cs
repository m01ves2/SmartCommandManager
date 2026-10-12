using SmartCommandManager.Modules.FileSystem.Commands.CopyCommand.NLP.Models;
using SmartCommandManager.Modules.FileSystem.Commands.CopyCommand.NLP.Validators;
using SmartCommandManager.Modules.FileSystem.Services;

namespace SmartCommandManager.Modules.FileSystem.Commands.CopyCommand.Builder
{
    public class CopyArgsBuilder
    {
        private readonly IFileSystemService _fs;

        public CopyArgsBuilder(IFileSystemService fs)
        {
            _fs = fs;
        }

        public CopyArgs Build(CopyParseTree tree)
        {
            FlagsCandidate flags = BuildFlags(tree);

            FlagSemanticsValidator.Validate(flags);

            SourceCandidate sourceCandidate = BuildSource(tree);
            
            DestinationCandidate destinationCandidate = BuildDestination(tree, sourceCandidate, flags);
            
            SourceDestinationCompatibilityValidator.Validate(sourceCandidate, destinationCandidate);
            
            return BuildCopyArgs(sourceCandidate, destinationCandidate, flags);
        }

        #region
        private SourceCandidate BuildSource(CopyParseTree tree)
        {
            SourceSyntaxValidator.Validate(tree);
            string path = ResolveSourcePath(tree);
            SourceCandidate candidate = BuildSourceCandidate(path);
            SourceSemanticsValidator.Validate(candidate);
            return candidate;
        }

        private string ResolveSourcePath(CopyParseTree tree)
        {
            return tree.SourcePaths.Paths.Single();
        }
        private SourceCandidate BuildSourceCandidate(string path)
        {
            // REVIEW: Относительный путь здесь проверяется относительно
            // рабочего каталога процесса, а команда разрешает его относительно
            // CommandContext.CurrentDirectory.
            // Нужно разрешать путь относительно контекста до проверки,
            // сохранять его в Candidate и Args и использовать при выполнении.
            ItemType sourceType = _fs.GetItemType(path);
            bool exists = sourceType != ItemType.NONE;

            return new SourceCandidate(path, sourceType, exists);
        }

        #endregion
        #region
        private DestinationCandidate BuildDestination(CopyParseTree tree, SourceCandidate source, FlagsCandidate flags)
        {
            DestinationSyntaxValidator.Validate(tree);
            string path = ResolveDestinationPath(tree); //вот тут проверяет, директория или файл, существует ли, нужно ли перезаписать
            DestinationCandidate destinationCandidate = BuildDestinationCandidate(path, source); //просто формируем обьект
            DestinationSemanticsValidator.Validate(destinationCandidate, flags);
            return destinationCandidate;
        }

        private string ResolveDestinationPath(CopyParseTree tree)
        {
            return tree.DestinationPaths.Paths.Single();
        }

        private DestinationCandidate BuildDestinationCandidate(string destinationPath, SourceCandidate source)
        {
            // REVIEW: Относительный путь здесь проверяется относительно
            // рабочего каталога процесса, а команда разрешает его относительно
            // CommandContext.CurrentDirectory.
            // Нужно разрешать путь относительно контекста до проверки,
            // сохранять его в Candidate и Args и использовать при выполнении.
            ItemType destinationType = _fs.GetItemType(destinationPath);

            if (destinationType == ItemType.DIRECTORY && source.ItemType == ItemType.FILE) {
                destinationPath = Path.Combine( destinationPath, Path.GetFileName(source.Path));//логика Path.Combine работает только если destination существует и является директорией.

                destinationType = _fs.GetItemType(destinationPath);
            }

            bool exists = destinationType != ItemType.NONE;

            return new DestinationCandidate(destinationPath, destinationType, exists);
        }

        #endregion
        #region
        private FlagsCandidate BuildFlags(CopyParseTree tree)
        {
            bool recursive = tree.Flags.Flags.Contains("recursive");
            bool overwrite = tree.Flags.Flags.Contains("overwrite");
            bool skipIfExists = tree.Flags.Flags.Contains("skip");
            bool hasWildcard = tree.Flags.Flags.Contains("wildcard");
            return new FlagsCandidate(recursive, overwrite, skipIfExists, hasWildcard);
        }
        #endregion
        private CopyArgs BuildCopyArgs(SourceCandidate source, DestinationCandidate destination, FlagsCandidate flags)
        {
            return new CopyArgs()
            {
                CopyMode = (source.ItemType == ItemType.FILE) ? CopyMode.File : CopyMode.Directory,
                SourcePath = source.Path,
                DestinationPath = destination.Path,
                Overwrite = flags.Overwrite,
                HasWildcard = flags.HasWildcard,
                Recursive = flags.Recursive,
                SkipIfExists = flags.SkipIfExists,
            };
        }
    }
}
