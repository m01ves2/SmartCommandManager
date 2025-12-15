using SmartCommandManager.NLP.Intent.Models;

namespace SmartCommandManager.Modules.FileSystem.Commands.CopyCommand
{
    public static class CopyIntentDescriptor
    {
        public static IntentDescriptor Descriptor =>
            new IntentDescriptor(
                Primary: "copy",
                Aliases : new[] { "copy", "cp", "duplicate", "clone" }
            );
    }
}
