using SmartCommandManager.NLP.Intent.Models;

namespace SmartCommandManager.NLP.Command.Models
{
    public class CopyParseTree
    {
        public IntentResult Intent { get; set; }
        public MarkerExtractionResult SourceMarkers { get; set; } = MarkerExtractionResult.Empty;
        public MarkerExtractionResult DestinationMarkers { get; set; } = MarkerExtractionResult.Empty;
        public PathExtractionResult SourcePaths { get; set; } = PathExtractionResult.Empty;
        public PathExtractionResult DestinationPaths { get; set; } = PathExtractionResult.Empty;
        public WildcardExtractionResult Wildcard { get; set; } = WildcardExtractionResult.None;
        public FlagExtractionResult Flags { get; set; } = FlagExtractionResult.None;
        public NoiseExtractionResult Noise { get; set; } = NoiseExtractionResult.None;

    }
}
