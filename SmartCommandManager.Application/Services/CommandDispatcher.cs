using Microsoft.Extensions.Logging;
using SmartCommandManager.Application.Exceptions;
using SmartCommandManager.Domain.Commands;
using SmartCommandManager.NLP.IntentNlp.Exceptions;
using SmartCommandManager.NLP.IntentNlp.Models;
using SmartCommandManager.NLP.IntentNlp.Parsers;
using SmartCommandManager.NLP.IntentNlp.Tokenizer;

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
            //_commandParser = commandParser;
            _tokenizer = tokenizer;
            _intentParser = intentParser; 
            _logger = logger;
        }

        public CommandResult Execute(string input)
        {
            try {

                IReadOnlyList<Token> tokens = _tokenizer.Tokenize(input);

                var intents = _commandRegistry.AllIntents;

                IntentResult  result = _intentParser.Parse(tokens, intents);

                ICommand command = _commandRegistry.Find(result.Intent);

                CommandResult commandResult = command.Execute(_commandContext);

                return commandResult;
            }
            catch (IntentNotFoundException ex) {
                CommandResult commandResult = new CommandResult() { Status = CommandStatus.NotFound, Message = $"Unknown command: {ex.Message}" };
                return commandResult;
            }
            catch (AmbiguousIntentException) {
                CommandResult commandResult = new CommandResult() { Status = CommandStatus.Conflict, Message = "Ambiguous command: please clarify" };
                return commandResult;
            }
            catch (IntentRepeatedException) {
                CommandResult commandResult = new CommandResult() { Status = CommandStatus.Conflict, Message = "Command repeated twice" };
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