using SmartCommandManager.NLP.IntentNlp.Models;

namespace SmartCommandManager.Modules.Thread.Commands
{
    public class PsIntentDescriptor
    {
        public static IntentDescriptor Descriptor =>
            new IntentDescriptor(
                Primary: "ps",
                Synonyms: new[] { "ps", "process", "processes", "thread", "threads" }
            );
    }
}