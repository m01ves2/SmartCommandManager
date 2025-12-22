using SmartCommandManager.Application.Services;
using SmartCommandManager.Domain.Commands.Models;

namespace SmartCommandManager.CompositionRoot
{
    public sealed class ApplicationGraph
    {
        public CommandDispatcher Dispatcher { get; }
        public CommandContext Context { get; }

        public ApplicationGraph(
            CommandDispatcher dispatcher,
            CommandContext context)
        {
            Dispatcher = dispatcher;
            Context = context;
        }
    }
}
