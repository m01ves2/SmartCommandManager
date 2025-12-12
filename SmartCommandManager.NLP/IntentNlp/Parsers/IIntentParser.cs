using SmartCommandManager.NLP.IntentNlp.Models;

namespace SmartCommandManager.NLP.IntentNlp.Parsers
{
    public interface IIntentParser
    {
        public IntentResult Parse(IReadOnlyList<Token> tokens, IEnumerable<IntentDescriptor> intents);
    }
}
