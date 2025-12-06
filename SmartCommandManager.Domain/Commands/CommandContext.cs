using SmartCommandManager.Domain.NLP;

namespace SmartCommandManager.Domain.Commands
{
    public sealed class CommandContext
    {
        public IReadOnlyList<Token> Tokens { get; }
        public string RawInput { get; }
        public string CurrentDirectory { get; set; } = Directory.GetCurrentDirectory();
        // TODO command history repository

        public CommandContext(IReadOnlyList<Token> tokens, string rawInput)
        {
            Tokens = tokens;
            RawInput = rawInput;
        }
    }
}
