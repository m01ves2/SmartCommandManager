using Microsoft.Extensions.Logging;
using SmartCommandManager.Application.Exceptions;
using SmartCommandManager.Domain.Commands.Models;
using SmartCommandManager.NLP.Args.Exceptions;
using SmartCommandManager.NLP.Intent.Exceptions;
using SmartCommandManager.NLP.Intent.Models;
using SmartCommandManager.NLP.Intent.Parsers;
using SmartCommandManager.NLP.Shared.Models;
using SmartCommandManager.NLP.Shared.Tokenizer;

namespace SmartCommandManager.Application.Services
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly CommandContext _commandContext;
        private readonly CommandRegistry _commandRegistry;
        private readonly IIntentParser _intentParser;
        private readonly ILogger<CommandDispatcher> _logger;
        private readonly ITokenizer _tokenizer;
        public CommandDispatcher(CommandContext commandContext, CommandRegistry commandRegistry, ITokenizer tokenizer, IIntentParser intentParser, ILogger<CommandDispatcher> logger)
        {
            _commandContext = commandContext;
            _commandRegistry = commandRegistry;
            _tokenizer = tokenizer;
            _intentParser = intentParser; 
            _logger = logger;
        }

        public CommandResult Execute(string input)
        {
            try {

                IReadOnlyList<Token> tokens = _tokenizer.Tokenize(input);

                IntentParseResult  intentParseResult = _intentParser.Parse(tokens, _commandRegistry.AllIntents);

                ICommandPipeline pipeline = _commandRegistry.Find(intentParseResult.Intent);

                CommandResult commandResult = pipeline.Execute(tokens, intentParseResult);

                return commandResult;
            }
            catch (IntentParsingException ex) {
                CommandResult commandResult = new CommandResult() { Status = CommandStatus.Failed, Message = $"Intent exception: {ex.Message}" };
                return commandResult;
            }
            catch(ArgsParsingException ex) {
                CommandResult commandResult = new CommandResult() { Status = CommandStatus.Failed, Message = $"Wrong commands arguments: {ex.Message}" };
                return commandResult;
            }
            catch (CommandNotFoundException ex) {
                CommandResult commandResult = new CommandResult() { Status = CommandStatus.NotFound, Message = $"Command not registered: {ex.Message}" };
                _logger.LogError($"Command not registered: {ex.Message}");
                return commandResult;
            }
            catch (InvalidOperationException ex) {
                CommandResult commandResult = new CommandResult() { Status = CommandStatus.Failed, Message = $"Operation error: {ex.Message}" };
                _logger.LogError($"Operation error: {ex.Message}");
                return commandResult;
            }
            catch (Exception ex) {
                CommandResult commandResult = new CommandResult() { Status = CommandStatus.Failed, Message = $"Unexpected error: {ex.Message}" };
                _logger.LogError($"Operation error: {ex.Message}");
                return commandResult;
            }
        }

        public string GetPrompt()
        {
            return _commandContext.CurrentDirectory;
        }
    }
}