
namespace SmartCommandManager.NLP.Args.Models
{
    public sealed class WildcardExtractionResult : IExtractionResult
    {
        bool HasWildcard { get; }
        public IReadOnlyCollection<int> Indexes { get; }

        public WildcardExtractionResult(bool hasWildcard, IReadOnlyCollection<int> indexes)
        {
            HasWildcard = hasWildcard;
            Indexes = indexes;
        }

        public static WildcardExtractionResult None => new(false, Array.Empty<int>());

    }
}
