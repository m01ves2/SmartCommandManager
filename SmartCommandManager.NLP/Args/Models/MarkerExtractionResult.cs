namespace SmartCommandManager.NLP.Args.Models
{
    public sealed class MarkerExtractionResult : IExtractionResult
    {
        public IReadOnlyCollection<int> Indexes { get; }

        public MarkerExtractionResult(IReadOnlyCollection<int> indexes)
        {
            Indexes = indexes;
        }

        public static MarkerExtractionResult Empty => new(Array.Empty<int>());
    }
}
