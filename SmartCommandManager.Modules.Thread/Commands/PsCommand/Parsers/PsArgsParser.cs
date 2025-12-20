using SmartCommandManager.Domain.Commands.Models;
using SmartCommandManager.NLP.Command.Parsers;
using SmartCommandManager.NLP.Intent.Models;
using SmartCommandManager.NLP.Shared.Models;

namespace SmartCommandManager.Modules.Thread.Commands.PsCommand.Parsers
{
    public class PsArgsParser : IArgsParser<Unit>
    {
        public Unit Parse(IReadOnlyList<Token> tokens, IntentParseResult intent)
        {
            return Unit.Value;
        }
    }
}
