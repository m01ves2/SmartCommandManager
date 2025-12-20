using SmartCommandManager.NLP.Intent.Models;

namespace SmartCommandManager.Modules.Thread.Commands.PsCommand
{
    public class PsIntentDescriptor
    {
        public static IntentDescriptor Descriptor =>
            new IntentDescriptor(
                Primary: "ps",
                Aliases : new[] { "ps", "process", "processes", "thread", "threads" }
            );
    }
}