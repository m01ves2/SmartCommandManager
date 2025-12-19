using SmartCommandManager.Modules.FileSystem.Commands.CopyCommand.Builder;
using SmartCommandManager.Modules.FileSystem.Services;
using SmartCommandManager.NLP.Args.Exceptions;

namespace SmartCommandManager.Modules.FileSystem.Commands.CopyCommand.NLP.Validators
{
    public static class SourceDestinationCompatibilityValidator //Validator of semantics between source and destination
    {
        public static void Validate(SourceCandidate source, DestinationCandidate destination)
        {
            if (source.ItemType == ItemType.DIRECTORY && destination.ItemType == ItemType.FILE) {
                throw new ValidationException($"Validation error: destination path must be a directory");
            }
        }
    }
}
