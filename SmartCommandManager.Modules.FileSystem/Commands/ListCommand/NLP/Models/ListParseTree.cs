using SmartCommandManager.NLP.Command.Models;
using SmartCommandManager.NLP.Intent.Models;

namespace SmartCommandManager.Modules.FileSystem.Commands.CopyCommand.NLP.Models
{
    public class ListParseTree
    {
        public IntentParseResult Intent { get; set; }
        public MarkerExtractionResult SourceMarkers { get; set; } = MarkerExtractionResult.Empty;
        public PathExtractionResult SourcePaths { get; set; } = PathExtractionResult.Empty;
        public WildcardExtractionResult Wildcard { get; set; } = WildcardExtractionResult.None;
        public FlagExtractionResult Flags { get; set; } = FlagExtractionResult.None;
        public NoiseExtractionResult Noise { get; set; } = NoiseExtractionResult.None;

    }
}
