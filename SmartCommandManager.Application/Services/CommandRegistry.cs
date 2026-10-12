using SmartCommandManager.Application.Exceptions;
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

        public IEnumerable<CommandHelpInfo> AllCommandsHelpInfo()
        {
            foreach (var entry in _intentCommandMap) {
                var aliases = entry.Key.Aliases
                    .Where(alias => alias != entry.Key.Primary)
                    .ToArray();

                yield return new CommandHelpInfo(entry.Value.CommandInfo, aliases);
            }
        }

        public IReadOnlyCollection<IntentDescriptor> AllIntents => _intentCommandMap.Keys;
    }
}