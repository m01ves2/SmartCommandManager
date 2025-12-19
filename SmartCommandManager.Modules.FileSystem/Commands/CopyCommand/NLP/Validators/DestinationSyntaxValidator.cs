using SmartCommandManager.Modules.FileSystem.Commands.CopyCommand.NLP.Models;
using SmartCommandManager.NLP.Args.Exceptions;
using SmartCommandManager.NLP.Args.Validators;

namespace SmartCommandManager.Modules.FileSystem.Commands.CopyCommand.NLP.Validators
{
    public static class DestinationSyntaxValidator
    {
        public static void Validate(CopyParseTree tree)
        {
            IReadOnlyList<string> paths = tree.DestinationPaths.Paths;

            if (paths.Count != 1)
                throw new ValidationException("Destination must contain exactly one path");

            NotNullOrEmptyValidator.Validate(paths[0],  "Destination path");

            // destination не wildcard, если не поддерживается
            if (string.Equals(paths[0], "*")) {
                throw new ValidationException($"Validation error: wildcard is not supported");
            }
        }
    }
}
