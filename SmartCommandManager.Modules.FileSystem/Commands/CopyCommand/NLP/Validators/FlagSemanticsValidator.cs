using SmartCommandManager.Modules.FileSystem.Commands.CopyCommand.Builder;
using SmartCommandManager.NLP.Args.Exceptions;

namespace SmartCommandManager.Modules.FileSystem.Commands.CopyCommand.NLP.Validators
{
    public static class FlagSemanticsValidator
    {
        public static void Validate(FlagsCandidate flags)
        {
            if (flags.Overwrite && flags.SkipIfExists) {
                throw new ValidationException("Validation error: overwrite and skip-if-exists cannot be used together");
            }

            if (flags.HasWildcard) {
                throw new ValidationException("Validation error: wildcard is not supported");
            }
        }
    }
}
