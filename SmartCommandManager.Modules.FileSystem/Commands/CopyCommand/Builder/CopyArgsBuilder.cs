using SmartCommandManager.Modules.FileSystem.Commands.CopyCommand.NLP.Models;
using SmartCommandManager.Modules.FileSystem.Commands.CopyCommand.NLP.Validators;
using SmartCommandManager.Modules.FileSystem.Services;
using SmartCommandManager.NLP.Args.Models;
using System.Numerics;

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
            SourceCandidate source = BuildSource(tree);
            DestinationCandidate destination = BuildDestination(tree);
            FlagsCandidate flags = BuildFlags(tree);


            return BuildCopyArgs(source, destination, flags);
        }

        private SourceCandidate BuildSource(CopyParseTree tree)
        {
            ValidateSourceSyntax(tree);

            string path = ResolveSourcePath(tree);

            SourceCandidate candidate = ResolveSourceCandidate(path);

            ValidateSourceSemantics(candidate);

            return candidate;
        }

        private DestinationCandidate BuildDestination(CopyParseTree tree)
        {
            //ValidateDestinationSyntax(tree)
            //candidate = ResolveDestination(tree, source)
            //ValidateDestinationSemantics(candidate, source)
            //return candidate

            throw new NotImplementedException();
        }

        private FlagsCandidate BuildFlags(CopyParseTree tree)
        {
            //flags = ResolveFlags(tree)
            //ValidateFlags(flags)
            //return flags

            throw new NotImplementedException();
        }

        private CopyArgs BuildCopyArgs(SourceCandidate source, DestinationCandidate destination, FlagsCandidate flags)
        {
            throw new NotImplementedException();
        }

        //private string BuildSourcePath(PathExtractionResult sourceExtractionResult)
        //{
        //    string sourcePath = sourceExtractionResult.Paths.FirstOrDefault() ?? "";

        //    return sourcePath;
        //}

        //private string BuildDestinationPath(PathExtractionResult destinationExtractionResult, string sourcePath)
        //{
        //    string destinationPath = destinationExtractionResult.Paths.FirstOrDefault() ?? "";

        //    if (_fs.GetItemType(destinationPath) == ItemType.DIRECTORY) {
        //        destinationPath = Path.Combine(destinationPath, sourcePath);
        //    }

        //    return destinationPath;

        //}

        //private bool BuildOverwriteFlag(FlagExtractionResult flagExtractionResult)
        //{
        //    return flagExtractionResult.Flags.Contains("overwrite");
        //}

        //private bool BuildRecursiveFlag(FlagExtractionResult flagExtractionResult)
        //{
        //    return flagExtractionResult.Flags.Contains("recursive");
        //}
    }
}
