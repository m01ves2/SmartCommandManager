using SmartCommandManager.NLP.Intent.Models;

namespace SmartCommandManager.NLP.Intent.Parsers
{
    public interface IIntentParser
    {
        public IntentResult Parse(IReadOnlyList<Token> tokens, IEnumerable<IntentDescriptor> intents);
    }
}
