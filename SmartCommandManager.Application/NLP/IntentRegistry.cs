using SmartCommandManager.NLP.Intent.Models;

namespace SmartCommandManager.Application.NLP
{
    public sealed class IntentRegistry
    {
        private readonly List<IntentDescriptor> _descriptors = new();

        public IReadOnlyList<IntentDescriptor> All => _descriptors;

        public void Register(IntentDescriptor descriptor) => _descriptors.Add(descriptor);
    }
}
