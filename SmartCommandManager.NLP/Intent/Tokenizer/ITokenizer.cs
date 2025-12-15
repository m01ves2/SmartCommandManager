using SmartCommandManager.NLP.Intent.Models;

namespace SmartCommandManager.NLP.Intent.Tokenizer
{
    public interface ITokenizer
    {
        IReadOnlyList<Token> Tokenize(string input);
    }
}
