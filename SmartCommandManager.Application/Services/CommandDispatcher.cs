using Microsoft.Extensions.Logging;
using ILogger = Microsoft.Extensions.Logging.ILogger; //to shrink Microsoft.Extensions.Logging namespace

using SmartCommandManager.Application.NLP;
using SmartCommandManager.Domain.Commands;
using SmartCommandManager.Domain.NLP;

namespace SmartCommandManager.Application.Services
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly CommandContext _commandContext;
        private readonly CommandRegistry _commandRegistry;
        //private readonly CommandParser _commandParser;
        private readonly IIntentNlpParser _nlp;
        private readonly ILogger<CommandDispatcher> _logger;
        private readonly ITokenizer _tokenizer;
        public CommandDispatcher(CommandContext commandContext, CommandRegistry commandRegistry, ITokenizer tokenizer, IIntentNlpParser nlp, ILogger<CommandDispatcher> logger)
        {
            _commandContext = commandContext;
            _commandRegistry = commandRegistry;
            //_commandParser = commandParser;
            _tokenizer = tokenizer;
            _nlp = nlp; 
            _logger = logger;
        }

        public CommandResult Execute(string input)
        {
            try {
                //(string commandName, string[] args) = _commandParser.Parse(input);
                //ICommand command = _commandRegistry.GetCommand(commandName);

                IReadOnlyList<Token> tokens = _tokenizer.Tokenize(input);
                //_commandContext.Tokens = tokens;
                string commandName = _nlp.Parse(tokens);
                ICommand command = _commandRegistry.GetCommand(commandName);

                CommandResult commandResult = command.Execute(_commandContext);
                return commandResult;
            }
            catch (InvalidOperationException ex) {
                CommandResult commandResult = new CommandResult() { Status = CommandStatus.Error, Message = $"Operation error: {ex.Message}" };
                _logger.LogError($"Operation error: {ex.Message}");
                return commandResult;
            }
            catch (Exception ex) {
                CommandResult commandResult = new CommandResult() { Status = CommandStatus.Error, Message = $"Unexpected error: {ex.Message}" };
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