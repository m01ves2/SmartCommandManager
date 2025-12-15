namespace SmartCommandManager.NLP.Command.Models
{
    public sealed record WildcardExtractionResult(bool HasWildcard)
    {
        public static readonly WildcardExtractionResult None = new WildcardExtractionResult(false);
    }
}
