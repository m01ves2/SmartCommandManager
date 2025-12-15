using SmartCommandManager.NLP.Shared.Models;

namespace SmartCommandManager.NLP.Shared.Tokenizer
{
    public interface ITokenizer
    {
        IReadOnlyList<Token> Tokenize(string input);
    }
}
