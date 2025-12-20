using SmartCommandManager.Modules.FileSystem.Commands.CopyCommand.Builder;
using SmartCommandManager.Modules.FileSystem.Commands.CopyCommand.NLP.Models;
using SmartCommandManager.Modules.FileSystem.Services;
using SmartCommandManager.NLP.Args.Models;
using SmartCommandManager.NLP.Command.Extractors;
using SmartCommandManager.NLP.Command.Parsers;
using SmartCommandManager.NLP.Intent.Models;
using SmartCommandManager.NLP.Shared.Models;
using System.Collections.ObjectModel;

namespace SmartCommandManager.Modules.FileSystem.Commands.CopyCommand.NLP.Parsers
{
    public sealed record FlagDescriptor(string Canonical, IReadOnlyList<string> Aliases);

    public class CopyArgsParser : IArgsParser<CopyArgs>
    {
        private readonly IFileSystemService _fs;
        public CopyArgsParser(IFileSystemService fs)
        {
            _fs = fs;
        }

        public CopyArgs Parse(IReadOnlyList<Token> tokens, IntentParseResult intent)
        {
            CopyParseTree tree = new CopyParseTree
            {
                Intent = intent,
            };

            tree.SourceMarkers = ExtractSourceMarkers(tokens, tree);
            tree.DestinationMarkers = ExtractDestinationMarkers(tokens, tree);
            tree.SourcePaths = ExtractSourcePaths(tokens, tree);
            tree.DestinationPaths = ExtractDestinationPaths(tokens, tree);
            tree.Flags = ExtractFlags(tokens, tree);
            tree.Wildcard = ExtractWildcard(tokens, tree);
            tree.Noise = ExtractNoise(tokens, tree);

            var copyArgsBuilder = new CopyArgsBuilder(_fs);
            CopyArgs copyArgs = copyArgsBuilder.Build(tree); 
            return copyArgs;
        }

        private MarkerExtractionResult ExtractSourceMarkers(IReadOnlyList<Token> tokens, CopyParseTree tree)
        {
            string[] sourceMarkers = ["from", "in", "inside"];
            int intentIndex = tree.Intent.IntentIndex;
            MarkerExtractionResult markerExtractionResult = MarkerExtractor.Extract(tokens, intentIndex, sourceMarkers);
            return markerExtractionResult;
        }

        private MarkerExtractionResult ExtractDestinationMarkers(IReadOnlyList<Token> tokens, CopyParseTree tree)
        {
            string[] destinationMarkers = ["to", "into"];
            int intentIndex = tree.Intent.IntentIndex;
            MarkerExtractionResult markerExtractionResult = MarkerExtractor.Extract(tokens, intentIndex, destinationMarkers);
            return markerExtractionResult;
        }


        private PathExtractionResult ExtractSourcePaths(IReadOnlyList<Token> tokens, CopyParseTree tree)
        {
            int defaultIndex = tree.GetFirstUnrecognizedIndex(tokens.Count);
            return ExtractPathsCore(
                tokens,
                tree.SourceMarkers.Indexes,
                tree.DestinationMarkers.Indexes,
                defaultIndex);
        }
        private PathExtractionResult ExtractDestinationPaths(IReadOnlyList<Token> tokens, CopyParseTree tree)
        {
            int defaultIndex = tree.GetFirstUnrecognizedIndex(tokens.Count);
            return ExtractPathsCore(
                tokens,
                tree.DestinationMarkers.Indexes,
                tree.SourceMarkers.Indexes,
                defaultIndex);
        }

        private PathExtractionResult ExtractPathsCore( 
            IReadOnlyList<Token> tokens, 
            IReadOnlyCollection<int> markersIndexes, 
            IReadOnlyCollection<int> destinationIndexes, 
            int defaultIndex)
        {
            var paths = new List<string>();
            var indexes = new Collection<int>();

            if (markersIndexes.Count > 0) {
                foreach (var markerIndex in markersIndexes) {
                    int candidateIndex = markerIndex + 1;

                    if (IsInvalidCandidate(candidateIndex, tokens.Count, markersIndexes, destinationIndexes))
                        paths.Add("");
                    else {
                        paths.Add(tokens[candidateIndex].Value);
                        indexes.Add(candidateIndex);
                    }
                }
            }
            else {
                if (IsInvalidCandidate(defaultIndex, tokens.Count, markersIndexes, destinationIndexes))
                    paths.Add("");
                else {
                    paths.Add(tokens[defaultIndex].Value);
                    indexes.Add(defaultIndex);
                }
            }

            return new PathExtractionResult(paths, indexes);
        }

        bool IsInvalidCandidate( int index, int tokenCount, IReadOnlyCollection<int> sourceMarkers, IReadOnlyCollection<int> destinationMarkers)
        {
            return index < 0 || index >= tokenCount || sourceMarkers.Contains(index) || destinationMarkers.Contains(index);
        }

        private FlagExtractionResult ExtractFlags(IReadOnlyList<Token> tokens, CopyParseTree tree)
        {
            int intentIndex = tree.Intent.IntentIndex;

            var flags = new List<FlagDescriptor>
            {
                new("recursive", new[] { "-r", "--recursive" }),
                new("overwrite", new[] { "-o", "--overwrite" })
            };

            var foundFlags = new List<string>();
            var foundIndexes = new List<int>();

            foreach (var flag in flags) {
                FlagExtractionResult flagExtractionResult = FlagExtractor.Extract(tokens, flag.Aliases);
                if (flagExtractionResult != FlagExtractionResult.Empty) {
                    foundFlags.AddRange(flagExtractionResult.Flags);
                    foundIndexes.AddRange(flagExtractionResult.Indexes);
                }
            }

            return new FlagExtractionResult(foundFlags, foundIndexes);
        }

        private WildcardExtractionResult ExtractWildcard(IReadOnlyList<Token> tokens, CopyParseTree tree)
        {
            string[] wildcardMarkers = ["*", "all"];
            int intentIndex = tree.Intent.IntentIndex;
            WildcardExtractionResult wildcardExtractionResult = WildcardExtractor.Extract(tokens, wildcardMarkers);
            return wildcardExtractionResult;
        }

        private NoiseExtractionResult ExtractNoise(IReadOnlyList<Token> tokens, CopyParseTree tree)
        {
            var indexes = tree.GetRecognizedIndexes();
            return new NoiseExtractionResult(indexes);
        }
    }
}
