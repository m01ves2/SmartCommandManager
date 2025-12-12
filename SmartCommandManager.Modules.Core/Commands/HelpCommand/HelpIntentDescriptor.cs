using SmartCommandManager.NLP.IntentNlp.Models;

namespace SmartCommandManager.Modules.Core.Commands.HelpCommand
{
    public class HelpIntentDescriptor
    {
        public static IntentDescriptor Descriptor =>
            new IntentDescriptor(
                Primary: "help",
                Synonyms: new[] { "help", "hlp", "man", "manual", "guide" }
            );
    }
}