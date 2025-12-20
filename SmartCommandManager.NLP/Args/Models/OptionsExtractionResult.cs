namespace SmartCommandManager.NLP.Args.Models
{ 
    public sealed class OptionsExtractionResult : IExtractionResult
    {
        IReadOnlyDictionary<string, string> Options { get; }
        public IReadOnlyCollection<int> Indexes { get; }

        public OptionsExtractionResult(IReadOnlyDictionary<string, string> options, IReadOnlyList<int> indexes)
        {
            Options = options;
            Indexes = indexes;
        }

        public static OptionsExtractionResult Empty => new(new Dictionary<string, string>(), Array.Empty<int>());
    }
}
