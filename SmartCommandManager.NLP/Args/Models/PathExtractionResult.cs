namespace SmartCommandManager.NLP.Args.Models
{
    public sealed class PathExtractionResult : IExtractionResult
    {
        public IReadOnlyList<string> Paths { get; }
        public IReadOnlyCollection<int> Indexes { get; }

        public PathExtractionResult(IReadOnlyList<string> paths, IReadOnlyCollection<int> indexes)
        {
            Paths = paths;
            Indexes = indexes;
        }

        public static PathExtractionResult Empty => new(Array.Empty<string>(), Array.Empty<int>());
    }
}
