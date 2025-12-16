using SmartCommandManager.Domain.Commands.Models;

namespace SmartCommandManager.Domain.Commands.Base
{
    public abstract class BaseCommand<TArgs>
    {
        public abstract CommandInfo CommandInfo { get; } // все наследники обязаны переопределять
       
        //public CommandResult Execute(object args)
        //{
        //    return Execute((TArgs)args);
        //}

        public abstract CommandResult Execute(TArgs args); //этот метод обязаны реализовать ВСЕ наследники. поэтому он abstract
    }
}