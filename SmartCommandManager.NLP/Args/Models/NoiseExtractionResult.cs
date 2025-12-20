namespace SmartCommandManager.NLP.Args.Models
{
    public sealed class NoiseExtractionResult : IExtractionResult
    {
        //public IReadOnlyList<string> Noise { get; }
        public IReadOnlyCollection<int> Indexes { get; }

        public NoiseExtractionResult(IReadOnlyCollection<int> indexes)
        {
            Indexes = indexes;
        }

        public static NoiseExtractionResult Empty => new(Array.Empty<int>());
    }
}

