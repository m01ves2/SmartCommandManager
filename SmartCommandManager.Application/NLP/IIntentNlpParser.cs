using SmartCommandManager.Domain.NLP;

namespace SmartCommandManager.Application.NLP
{
    public interface IIntentNlpParser
    {
        string Parse(IEnumerable<Token> tokens);
    }
}
