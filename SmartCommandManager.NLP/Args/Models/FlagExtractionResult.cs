namespace SmartCommandManager.NLP.Args.Models
{
    public sealed class FlagExtractionResult : IExtractionResult
    {
        public IReadOnlyList<string> Flags { get; }
        public IReadOnlyCollection<int> Indexes { get; }

        public FlagExtractionResult(IReadOnlyList<string> flags, IReadOnlyCollection<int> indexes)
        {
            Flags = flags;
            Indexes = indexes;
        }

        public static FlagExtractionResult Empty => new(Array.Empty<string>(), Array.Empty<int>());
    }
}