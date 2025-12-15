namespace SmartCommandManager.NLP.Command.Models
{
    public sealed record MarkerExtractionResult(IReadOnlyList<int> Indexes)
    {
        public static readonly MarkerExtractionResult Empty = new MarkerExtractionResult(Array.Empty<int>());
    }
}
