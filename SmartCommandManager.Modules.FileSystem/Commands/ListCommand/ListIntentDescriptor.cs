using SmartCommandManager.NLP.IntentNlp.Models;

namespace SmartCommandManager.Modules.FileSystem.Commands.ListCommand
{
    public class ListIntentDescriptor
    {
        public static IntentDescriptor Descriptor =>
            new IntentDescriptor(
                Primary: "list",
                Synonyms: new[] { "list", "ls", "show", "reveal" }
            );
    }
}