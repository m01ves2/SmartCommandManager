using SmartCommandManager.Application.Exceptions;
using SmartCommandManager.Domain.Commands;
using SmartCommandManager.Domain.NLP;

namespace SmartCommandManager.Application.NLP
{
    public class IntentNlpParser : IIntentNlpParser
    {
        public NlpIntentResult Parse(IReadOnlyList<Token> tokens, IEnumerable<ICommand> commands)
        { 
            //TODO
            foreach (Token token in tokens) {
                foreach (var command in commands) {
                    if(command.IntentPattern.Synonyms.Contains(token.Value))
                        return new NlpIntentResult(token.Value, -1);
                }
            }

            throw new IntentNotFoundException("Input doesn't contain intent");
        }
    }
}
