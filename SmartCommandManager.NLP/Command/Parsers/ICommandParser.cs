using SmartCommandManager.NLP.Intent.Models;

namespace SmartCommandManager.NLP.Command.Parsers
{
    public interface ICommandParser<TArgs>
    {
        TArgs Parse(IReadOnlyList<Token> tokens, int intentIndex);
    }
}
