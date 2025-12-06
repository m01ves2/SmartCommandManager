using SmartCommandManager.Domain.NLP;

namespace SmartCommandManager.Application.NLP.Validators
{
    public interface IIntentValidator
    {
        bool IsValid(IntentPattern pattern, IReadOnlyList<Token> tokens);
    }
}
