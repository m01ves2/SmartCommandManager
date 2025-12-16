using SmartCommandManager.Domain.Commands.Base;
using SmartCommandManager.Domain.Commands.Models;
using SmartCommandManager.NLP.Command.Parsers;
using SmartCommandManager.NLP.Intent.Models;
using SmartCommandManager.NLP.Shared.Models;

namespace SmartCommandManager.Application.Services
{
    public sealed class CommandPipeline<TArgs> : ICommandPipeline
    {
        private readonly BaseCommand<TArgs> _command;
        private readonly IArgsParser<TArgs> _parser;

        public CommandPipeline( BaseCommand<TArgs> command, IArgsParser<TArgs> parser)
        {
            _command = command;
            _parser = parser;
        }

        public CommandInfo CommandInfo => _command.CommandInfo;

        public CommandResult Execute(IReadOnlyList<Token> tokens, IntentParseResult intent)
        {
            TArgs args = _parser.Parse(tokens, intent);
            return _command.Execute(args);
        }
    }
}
