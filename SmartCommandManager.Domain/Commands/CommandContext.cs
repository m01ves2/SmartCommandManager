using SmartCommandManager.Domain.NLP;

namespace SmartCommandManager.Domain.Commands
{
    public sealed class CommandContext
    {
        public IReadOnlyList<Token> Tokens { get; }
        public string RawInput { get; }

        public CommandContext(IReadOnlyList<Token> tokens, string rawInput)
        {
            Tokens = tokens;
            RawInput = rawInput;
        }
    }
}
