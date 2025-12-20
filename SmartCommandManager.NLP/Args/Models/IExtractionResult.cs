namespace SmartCommandManager.NLP.Args.Models
{
    public interface IExtractionResult
    {
        IReadOnlyCollection<int> Indexes { get; }
    }
}
