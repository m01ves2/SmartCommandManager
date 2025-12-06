using SmartCommandManager.Domain.NLP;

namespace SmartCommandManager.Application.NLP
{
    public interface ITokenizer
    {
        IReadOnlyList<Token> Tokenize(string input);
    }
}
