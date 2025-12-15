using SmartCommandManager.NLP.Intent.Models;

namespace SmartCommandManager.Modules.Core.Commands.ExitCommand
{
    public static class ExitIntentDescriptor
    {
        public static IntentDescriptor Descriptor =>
            new IntentDescriptor(
                Primary: "exit",
                Aliases : new[] { "exit", "quit", "leave", "abandon" }
            );
    }
}

