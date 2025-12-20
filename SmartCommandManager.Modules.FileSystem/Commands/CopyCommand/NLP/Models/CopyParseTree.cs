using SmartCommandManager.NLP.Args.Exceptions;
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
        public FlagExtractionResult Flags { get; set; } = FlagExtractionResult.Empty;
        public NoiseExtractionResult Noise { get; set; } = NoiseExtractionResult.Empty;

        public IReadOnlySet<int> GetRecognizedIndexes()
        {
            return new HashSet<int>(
                new[]
                {
                    Intent.IntentIndex
                }
                .Concat(SourceMarkers.Indexes)
                .Concat(DestinationMarkers.Indexes)
                .Concat(SourcePaths.Indexes)
                .Concat(DestinationPaths.Indexes)
                .Concat(Flags.Indexes)
                .Concat(Wildcard.Indexes)
            );
        }

        public int GetFirstUnrecognizedIndex(int tokensCount)
        {
            var recognized = GetRecognizedIndexes();

            for (int i = 0; i < tokensCount; i++) {
                if (!recognized.Contains(i))
                    return i;
            }

            //throw new ArgsParsingException("source path or destination path is missing");
            return -1;
        }

    }
}
