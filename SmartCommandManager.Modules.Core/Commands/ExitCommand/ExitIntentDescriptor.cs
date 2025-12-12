using SmartCommandManager.NLP.IntentNlp.Models;

namespace SmartCommandManager.Modules.Core.Commands.ExitCommand
{
    public static class ExitIntentDescriptor
    {
        public static IntentDescriptor Descriptor =>
            new IntentDescriptor(
                Primary: "exit",
                Synonyms: new[] { "exit", "quit", "leave", "abandon" }
            );
    }
}

