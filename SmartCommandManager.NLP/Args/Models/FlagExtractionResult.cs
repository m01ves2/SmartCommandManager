namespace SmartCommandManager.NLP.Args.Models
{
    public sealed record FlagExtractionResult(IReadOnlyList<string> Flags)
    {
        public static readonly FlagExtractionResult None = new FlagExtractionResult(Array.Empty<string>());
    }
}