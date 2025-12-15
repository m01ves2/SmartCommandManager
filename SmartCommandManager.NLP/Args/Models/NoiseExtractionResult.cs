namespace SmartCommandManager.NLP.Command.Models
{
    public sealed record NoiseExtractionResult(  IReadOnlyList<string> Tokens )
    {
        public static readonly NoiseExtractionResult None =  new NoiseExtractionResult(Array.Empty<string>());
    }
}

