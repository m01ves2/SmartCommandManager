using SmartCommandManager.NLP.Intent.Models;
using SmartCommandManager.NLP.Shared.Models;

namespace SmartCommandManager.NLP.Intent.Parsers
{
    public interface IIntentParser
    {
        public IntentParseResult Parse(IReadOnlyList<Token> tokens, IEnumerable<IntentDescriptor> intents);
    }
}
