namespace SmartCommandManager.Application.Dispatcher
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly CommandContext _commandContext;
        private readonly CommandRegistry _commandRegistry;
        //private readonly CommandParser _commandParser;
        private readonly INlpParser _nlp;
        private readonly ILogger<CommandDispatcher> _logger;
        private readonly ITokenizer _tokenizer;
        public CommandDispatcher(CommandContext commandContext, CommandRegistry commandRegistry, ITokenizer tokenizer, INlpParser nlp, ILogger<CommandDispatcher> logger)
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
                (ICommand command, IEnumerable<Token> argTokens) = _nlp.Parse(tokens);

                CommandResult commandResult = command.Execute(argTokens);
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