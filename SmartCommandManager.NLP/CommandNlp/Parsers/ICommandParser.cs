using SmartCommandManager.NLP.IntentNlp.Models;

namespace SmartCommandManager.NLP.CommandNlp.Parsers
{
    public interface ICommandParser<TArgs>
    {
        TArgs Parse(IEnumerable<Token> tokens);
    }
}
