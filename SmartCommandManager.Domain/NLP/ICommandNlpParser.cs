using SmartCommandManager.Domain.Commands;

namespace SmartCommandManager.Domain.NLP
{
    public interface ILocalNlpParser<TArgs>
    {
        TArgs Parse(IEnumerable<Token> tokens);
    }
}
