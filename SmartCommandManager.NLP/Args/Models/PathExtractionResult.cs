namespace SmartCommandManager.NLP.Args.Models
{
    public sealed record PathExtractionResult(IReadOnlyList<string> Paths)
    {
        public static readonly PathExtractionResult Empty = new PathExtractionResult(Array.Empty<string>());
    }
}
