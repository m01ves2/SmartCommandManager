using SmartCommandManager.NLP.Intent.Models;

namespace SmartCommandManager.Modules.Core.Commands.HelpCommand
{
    public class HelpIntentDescriptor
    {
        public static IntentDescriptor Descriptor =>
            new IntentDescriptor(
                Primary: "help",
                Aliases : new[] { "help", "hlp", "man", "manual", "guide" }
            );
    }
}