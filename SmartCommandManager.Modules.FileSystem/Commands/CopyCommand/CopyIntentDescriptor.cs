using SmartCommandManager.NLP.IntentNlp.Models;

namespace SmartCommandManager.Modules.FileSystem.Commands.CopyCommand
{
    public static class CopyIntentDescriptor
    {
        public static IntentDescriptor Descriptor =>
            new IntentDescriptor(
                Primary: "copy",
                Synonyms: new[] { "copy", "cp", "duplicate", "clone" }
            );
    }
}
