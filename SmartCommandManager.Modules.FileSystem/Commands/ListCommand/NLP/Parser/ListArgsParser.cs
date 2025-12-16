using SmartCommandManager.Modules.FileSystem.Commands.CopyCommand.NLP.Models;
using SmartCommandManager.NLP.Command.Extractors;
using SmartCommandManager.NLP.Command.Models;
using SmartCommandManager.NLP.Command.Parsers;
using SmartCommandManager.NLP.Intent.Models;
using SmartCommandManager.NLP.Shared.Models;

namespace SmartCommandManager.Modules.FileSystem.Commands.CopyCommand.NLP.Parsers
{
    public sealed record FlagDescriptor(string Canonical, IReadOnlyList<string> Aliases);

    public class ListArgsParser : IArgsParser<CopyArgs>
    {
        public CopyArgs Parse(IReadOnlyList<Token> tokens, IntentParseResult intent)
        {
            //1. Make parse tree

            CopyParseTree tree = new CopyParseTree
            {
                Intent = intent,
            };

            var sourceMarkers = ExtractSourceMarkers(tokens, tree);
            var destinationMarkers = ExtractDestinationMarkers(tokens, tree);

            tree.SourceMarkers = sourceMarkers;
            tree.DestinationMarkers = destinationMarkers;
            tree.SourcePaths = ExtractSourcePaths(tokens, tree);
            tree.DestinationPaths = ExtractDestinationPaths(tokens, tree);
            tree.Flags = ExtractFlags(tokens, tree);
            tree.Wildcard = ExtractWildcard(tokens, tree);
            tree.Noise = NoiseExtractionResult.None; //while no need to ExtractNoise

            //2. Validate parsed tree
            //Validate(tree);

            //3. Make command context - Args
            //return CopyBuilder.Build(tree);
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
            var paths = ExtractPathsCore(
                tokens,
                tree.SourceMarkers.Indexes,
                tree.DestinationMarkers.Indexes,
                tree.Intent.IntentIndex + 1);

            return new PathExtractionResult(paths);
        }
        private PathExtractionResult ExtractDestinationPaths(IReadOnlyList<Token> tokens, CopyParseTree tree)
        {
            var paths = ExtractPathsCore(
                tokens,
                tree.DestinationMarkers.Indexes,
                tree.SourceMarkers.Indexes,
                tree.Intent.IntentIndex + 2);

            return new PathExtractionResult(paths);
        }

        private IReadOnlyList<string> ExtractPathsCore(
            IReadOnlyList<Token> tokens,
            IReadOnlyList<int> markers,
            IReadOnlyList<int> forbiddenIndexes,
            int defaultIndex)
        {
            var paths = new List<string>();

            if (markers.Count > 0) {
                foreach (var markerIndex in markers) {
                    int candidate = markerIndex + 1;

                    if (IsInvalidCandidate(candidate, tokens.Count, forbiddenIndexes, markers))
                        paths.Add("");
                    else
                        paths.Add(tokens[candidate].Value);
                }
            }
            else {
                if (IsInvalidCandidate(defaultIndex, tokens.Count, forbiddenIndexes, markers))
                    paths.Add("");
                else
                    paths.Add(tokens[defaultIndex].Value);
            }

            return paths;
        }

        bool IsInvalidCandidate(int index, int tokenCount, IReadOnlyCollection<int> sourceMarkers, IReadOnlyCollection<int> destinationMarkers)
        {
            return index >= tokenCount || sourceMarkers.Contains(index) || destinationMarkers.Contains(index);
        }

        private FlagExtractionResult ExtractFlags(IReadOnlyList<Token> tokens, CopyParseTree tree)
        {
            int intentIndex = tree.Intent.IntentIndex;

            var flags = new List<FlagDescriptor>
            {
                new("recursive", new[] { "-r", "--recursive" }),
                new("overwrite", new[] { "-o", "--overwrite" })
            };

            var found = new List<string>();

            foreach (var flag in flags) {
                if (FlagExtractor.Extract(tokens, flag.Aliases) != FlagExtractionResult.None) {
                    found.Add(flag.Canonical);
                }
            }

            return new FlagExtractionResult(found);
        }

        private WildcardExtractionResult ExtractWildcard(IReadOnlyList<Token> tokens, CopyParseTree tree)
        {
            string[] wildcardMarkers = ["*", "all"];
            int intentIndex = tree.Intent.IntentIndex;
            WildcardExtractionResult wildcardExtractionResult = WildcardExtractor.Extract(tokens, intentIndex, wildcardMarkers);
            return wildcardExtractionResult;
        }

        private NoiseExtractionResult ExtractNoise(IReadOnlyList<Token> tokens, CopyParseTree tree)
        {
            throw new NotImplementedException();
        }

        //private void Validate(IReadOnlyList<Token>  tokens, CopyParseTree tree, CopyArgs copyArgs)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
