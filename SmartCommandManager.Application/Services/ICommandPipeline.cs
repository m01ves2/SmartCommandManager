using SmartCommandManager.Domain.Commands.Models;
using SmartCommandManager.NLP.Intent.Models;
using SmartCommandManager.NLP.Shared.Models;

namespace SmartCommandManager.Application.Services
{
    public interface ICommandPipeline
    {
        CommandInfo CommandInfo { get; }
        CommandResult Execute(IReadOnlyList<Token> tokens, IntentParseResult intent); // Универсальный вызов
    }
}
