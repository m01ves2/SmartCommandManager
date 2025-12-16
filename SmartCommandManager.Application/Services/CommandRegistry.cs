using SmartCommandManager.Application.Exceptions;
using SmartCommandManager.Domain.Commands.Base;
using SmartCommandManager.Domain.Commands.Models;
using SmartCommandManager.NLP.Intent.Models;

namespace SmartCommandManager.Application.Services
{
    /// <summary>
    /// Registry for Commands
    /// </summary>
    public sealed class CommandRegistry
    {
        private readonly Dictionary<IntentDescriptor, ICommandPipeline> _intentCommandMap = new();

        public void Register(IntentDescriptor intent, ICommandPipeline command)
        {
            _intentCommandMap[intent] = command;
        }

        public ICommandPipeline Find(IntentDescriptor intent)
        {
            if (!_intentCommandMap.TryGetValue(intent, out var cmd))
                throw new CommandNotFoundException(intent.Primary);
            return cmd;
        }

        public IEnumerable<CommandInfo> AllCommandsInfo()
        {
            foreach (var cmd in _intentCommandMap.Values)
                yield return cmd.CommandInfo;
        }

        public IReadOnlyCollection<IntentDescriptor> AllIntents => _intentCommandMap.Keys;
    }
}