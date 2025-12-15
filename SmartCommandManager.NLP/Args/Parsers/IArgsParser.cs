using SmartCommandManager.NLP.Shared.Models;

namespace SmartCommandManager.NLP.Command.Parsers
{
    public interface IArgsParser<TArgs>
    {
        TArgs Parse(IReadOnlyList<Token> tokens, int intentIndex);
    }
}
