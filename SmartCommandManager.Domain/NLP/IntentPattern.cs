namespace SmartCommandManager.Domain.NLP
{
    public sealed class IntentPattern
    {
        public string Primary { get; } //canonical name of command
        public IReadOnlyCollection<string> Synonyms { get; }

        //public IReadOnlyCollection<IIntentValidator> Validators { get; }

        public IntentPattern(string primary, IEnumerable<string> synonyms)
        { 
            Primary = primary;
            Synonyms = synonyms.ToList();
        }
    }
}
