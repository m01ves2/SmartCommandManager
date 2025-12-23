using SmartCommandManager.Application.Services;
using SmartCommandManager.Domain.Commands.Models;

namespace SmartCommandManager.CompositionRoot
{
    public sealed class ApplicationRoot
    {
        public CommandDispatcher Dispatcher { get; }
        public CommandContext Context { get; }
        public ApplicationRoot(
            CommandDispatcher dispatcher,
            CommandContext context)
        {
            Dispatcher = dispatcher;
            Context = context;
        }
    }
}
