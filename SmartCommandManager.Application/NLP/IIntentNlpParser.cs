using SmartCommandManager.Domain.Commands;
using SmartCommandManager.Domain.NLP;


namespace SmartCommandManager.Application.NLP
{
    public interface IIntentNlpParser
    {
        public NlpIntentResult Parse(IReadOnlyList<Token> tokens, IEnumerable<ICommand> commands);
    }
}
