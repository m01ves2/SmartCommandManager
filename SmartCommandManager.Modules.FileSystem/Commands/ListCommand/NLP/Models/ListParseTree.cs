using SmartCommandManager.NLP.Args.Exceptions;
using SmartCommandManager.NLP.Args.Models;
using SmartCommandManager.NLP.Intent.Models;

namespace SmartCommandManager.Modules.FileSystem.Commands.ListCommand.NLP.Models
{
    public class ListParseTree
    {
        public IntentParseResult Intent { get; set; }

        public MarkerExtractionResult SourceMarkers { get; set; } = MarkerExtractionResult.Empty;
        public PathExtractionResult SourcePaths { get; set; } = PathExtractionResult.Empty;
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
                .Concat(SourcePaths.Indexes)
                .Concat(Flags.Indexes)
            );
        }

        public int GetFirstUnrecognizedIndex(int tokensCount)
        {
            var recognized = GetRecognizedIndexes();

            for (int i = 0; i < tokensCount; i++) {
                if (!recognized.Contains(i))
                    return i;
            }

            //throw new ArgsParsingException("source path path is missing");
            return -1;
        }
    }
}
