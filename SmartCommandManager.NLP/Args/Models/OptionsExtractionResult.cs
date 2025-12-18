namespace SmartCommandManager.NLP.Args.Models
{ 
    public sealed record OptionsExtractionResult(IReadOnlyDictionary<string, string> Options)
    {
        public static readonly OptionsExtractionResult None = new OptionsExtractionResult(new Dictionary<string, string>());
    }
}
