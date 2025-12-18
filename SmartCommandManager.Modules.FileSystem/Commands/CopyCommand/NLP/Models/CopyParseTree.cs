using SmartCommandManager.NLP.Args.Models;
using SmartCommandManager.NLP.Intent.Models;

namespace SmartCommandManager.Modules.FileSystem.Commands.CopyCommand.NLP.Models
{
    public class CopyParseTree
    {
        public IntentParseResult Intent { get; set; }
        public MarkerExtractionResult SourceMarkers { get; set; } = MarkerExtractionResult.Empty;
        public MarkerExtractionResult DestinationMarkers { get; set; } = MarkerExtractionResult.Empty;
        public PathExtractionResult SourcePaths { get; set; } = PathExtractionResult.Empty;
        public PathExtractionResult DestinationPaths { get; set; } = PathExtractionResult.Empty;
        public WildcardExtractionResult Wildcard { get; set; } = WildcardExtractionResult.None;
        public FlagExtractionResult Flags { get; set; } = FlagExtractionResult.None;
        public NoiseExtractionResult Noise { get; set; } = NoiseExtractionResult.None;

    }
}
