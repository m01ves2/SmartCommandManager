using SmartCommandManager.Application.Exceptions;
using SmartCommandManager.Domain.Commands;
using SmartCommandManager.NLP.IntentNlp.Models;

namespace SmartCommandManager.Application.Services
{
    /// <summary>
    /// Registry for Commands
    /// </summary>
    public sealed class CommandRegistry
    {
        private readonly Dictionary<IntentDescriptor, ICommand> _intentCommandMap = new();

        public void Register(IntentDescriptor intent, ICommand command)
        {
            _intentCommandMap[intent] = command;
        }

        public ICommand Find(IntentDescriptor intent)
        {
            if (!_intentCommandMap.TryGetValue(intent, out var cmd))
                throw new CommandNotFoundException(intent.Primary);
            return cmd;
        }

        public IReadOnlyCollection<ICommand> AllCommands => _intentCommandMap.Values;
        public IReadOnlyCollection<IntentDescriptor> AllIntents => _intentCommandMap.Keys;
    }
}