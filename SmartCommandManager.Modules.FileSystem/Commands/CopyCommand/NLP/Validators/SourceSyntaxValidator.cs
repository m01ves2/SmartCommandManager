using SmartCommandManager.Modules.FileSystem.Commands.CopyCommand.NLP.Models;
using SmartCommandManager.NLP.Args.Exceptions;
using SmartCommandManager.NLP.Args.Validators;

namespace SmartCommandManager.Modules.FileSystem.Commands.CopyCommand.NLP.Validators
{
    public static class SourceSyntaxValidator
    {
        public static void Validate(CopyParseTree tree)
        {
            IReadOnlyList<string> paths = tree.SourcePaths.Paths;

            if (paths.Count != 1)
                throw new ValidationException("Source must contain exactly one path");

            NotNullOrEmptyValidator.Validate(paths[0], "Source path");

            // source не wildcard, если не поддерживается
            if ( string.Equals(paths[0], "*")) {
                throw new ValidationException($"Validation error: wildcard is not supported");
            }
        }
    }
}
