using SmartCommandManager.Domain.NLP;
using SmartCommandManager.Domain.NLP.Validators;

namespace SmartCommandManager.Modules.FileSystem.Commands.CopyCommand
{
    public class CopyNlpParser : ILocalNlpParser<CopyArgs>
    {
        IEnumerable<IValidator<CopyArgs>> _validators;
        public CopyNlpParser(IEnumerable<IValidator<CopyArgs>> validators)
        {
            _validators = validators.ToList();
        }

        public CopyArgs Parse(IEnumerable<Token> tokens)
        {
            throw new NotImplementedException();
        }
    }
}
