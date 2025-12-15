using SmartCommandManager.NLP.Intent.Models;

namespace SmartCommandManager.Modules.FileSystem.Commands.ListCommand
{
    public class ListIntentDescriptor
    {
        public static IntentDescriptor Descriptor =>
            new IntentDescriptor(
                Primary: "list",
                Aliases : new[] { "list", "ls", "show", "reveal" }
            );
    }
}