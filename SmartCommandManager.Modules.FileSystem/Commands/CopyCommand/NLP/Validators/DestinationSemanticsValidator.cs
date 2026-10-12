using SmartCommandManager.Modules.FileSystem.Commands.CopyCommand.Builder;
using SmartCommandManager.Modules.FileSystem.Commands.CopyCommand.NLP.Models;
using SmartCommandManager.NLP.Args.Exceptions;

namespace SmartCommandManager.Modules.FileSystem.Commands.CopyCommand.NLP.Validators
{
    public static class DestinationSemanticsValidator
    {
        public static void Validate(DestinationCandidate candidate, FlagsCandidate flags)
        {
            if (candidate.Exists && !flags.Overwrite && !flags.SkipIfExists) {
                throw new ValidationException("Validation error: Destination already exists; use overwrite or skip-if-exists");
            }

            if (candidate.ItemType == Services.ItemType.DIRECTORY && flags.Overwrite)
                throw new ValidationException("Validation error: \"Overwrite\" flag is not available for directory");

            if (candidate.ItemType == Services.ItemType.FILE && flags.Recursive)
                throw new ValidationException("Validation error: \"Recursive\" flag is not available for file");
        }
    }
}
