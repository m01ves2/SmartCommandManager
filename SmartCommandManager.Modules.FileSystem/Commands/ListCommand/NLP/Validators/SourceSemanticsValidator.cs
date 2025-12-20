using SmartCommandManager.Modules.FileSystem.Commands.ListCommand.Builder;
using SmartCommandManager.Modules.FileSystem.Services;
using SmartCommandManager.NLP.Args.Exceptions;

namespace SmartCommandManager.Modules.FileSystem.Commands.ListCommand.NLP.Validators
{
    public static class SourceSemanticsValidator
    {
        public static void Validate(SourceCandidate candidate)
        {
            if (!candidate.Exists)
                throw new ValidationException("Validation error: source file or directory does not exist");

            if(candidate.ItemType != ItemType.FILE && candidate.ItemType != ItemType.DIRECTORY) {
                throw new ValidationException("Validation error: unknown source type. Not file or directory");
            }
        }
    }
}
