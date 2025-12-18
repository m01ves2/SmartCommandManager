namespace SmartCommandManager.NLP.Args.Models
{
    public sealed record WildcardExtractionResult(bool HasWildcard)
    {
        public static readonly WildcardExtractionResult None = new WildcardExtractionResult(false);
    }
}
