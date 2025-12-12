using SmartCommandManager.NLP.IntentNlp.Models;

namespace SmartCommandManager.NLP.IntentNlp.Tokenizer
{
    public interface ITokenizer
    {
        IReadOnlyList<Token> Tokenize(string input);
    }
}
