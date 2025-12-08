using SmartCommandManager.Domain.NLP;

namespace SmartCommandManager.Domain.Commands
{
    public abstract class BaseCommand<TArgs> : ICommand
    {
        public abstract CommandInfo CommandInfo { get; } // все наследники обязаны переопределять
        
        public virtual IntentPattern IntentPattern { get; } = new IntentPattern(string.Empty, Array.Empty<string>()); //virtual потому что наследники не обязаны переопределять

        public CommandResult Execute(object args)
        {
            return Execute((TArgs)args);
        }

        protected abstract CommandResult Execute(TArgs args); //этот метод обязаны реализовать ВСЕ наследники. поэтому он abstract
    }
}