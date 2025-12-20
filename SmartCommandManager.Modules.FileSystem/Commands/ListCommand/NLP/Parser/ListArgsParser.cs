using SmartCommandManager.Modules.FileSystem.Commands.CopyCommand.NLP.Models;
using SmartCommandManager.Modules.FileSystem.Commands.ListCommand;
using SmartCommandManager.Modules.FileSystem.Commands.ListCommand.Builder;
using SmartCommandManager.Modules.FileSystem.Commands.ListCommand.NLP.Models;
using SmartCommandManager.Modules.FileSystem.Services;
using SmartCommandManager.NLP.Args.Models;
using SmartCommandManager.NLP.Command.Extractors;
using SmartCommandManager.NLP.Command.Parsers;
using SmartCommandManager.NLP.Intent.Models;
using SmartCommandManager.NLP.Shared.Models;
using System;
using System.Collections.ObjectModel;

namespace SmartCommandManager.Modules.FileSystem.Commands.ListCommand.NLP.Parsers
{
    public sealed record FlagDescriptor(string Canonical, IReadOnlyList<string> Aliases);

    public class ListArgsParser : IArgsParser<ListArgs>
    {
        private readonly IFileSystemService _fs;

        public ListArgsParser(IFileSystemService fs)
        {
            _fs = fs;
        }

        public ListArgs Parse(IReadOnlyList<Token> tokens, IntentParseResult intent)
        {
            ListParseTree tree = new ListParseTree
            {
                Intent = intent,
            };

            tree.SourceMarkers = ExtractSourceMarkers(tokens, tree);
            tree.SourcePaths = ExtractSourcePaths(tokens, tree);
            tree.Flags = ExtractFlags(tokens, tree);
            tree.Noise = ExtractNoise(tokens, tree);

            var listArgsBuilder = new ListArgsBuilder(_fs);
            ListArgs listArgs = listArgsBuilder.Build(tree);
            return listArgs;
        }

        private MarkerExtractionResult ExtractSourceMarkers(IReadOnlyList<Token> tokens, ListParseTree tree)
        {
            string[] sourceMarkers = ["in", "inside"];
            int intentIndex = tree.Intent.IntentIndex;
            MarkerExtractionResult markerExtractionResult = MarkerExtractor.Extract(tokens, intentIndex, sourceMarkers);
            return markerExtractionResult;
        }

        private PathExtractionResult ExtractSourcePaths(IReadOnlyList<Token> tokens, ListParseTree tree)
        {
            var paths = new List<string>();
            var indexes = new Collection<int>();
            var markers = tree.SourceMarkers.Indexes;
            int defaultIndex = tree.GetFirstUnrecognizedIndex(tokens.Count);

            if (markers.Count > 0) {
                foreach (var markerIndex in markers) {
                    int candidateIndex = markerIndex + 1;

                    if (IsInvalidCandidate(candidateIndex, tokens.Count, markers))
                        paths.Add("");
                    else {
                        paths.Add(tokens[candidateIndex].Value);
                        indexes.Add(candidateIndex);
                    }
                }
            }
            else {
                if (IsInvalidCandidate(defaultIndex, tokens.Count, markers))
                    paths.Add(".");
                else {
                    paths.Add(tokens[defaultIndex].Value);
                    indexes.Add(defaultIndex);
                }
            }

            return new PathExtractionResult(paths, indexes);
        }

        bool IsInvalidCandidate(int index, int tokenCount, IReadOnlyCollection<int> sourceMarkers)
        {
            return index < 0 || index >= tokenCount || sourceMarkers.Contains(index);
        }

        private FlagExtractionResult ExtractFlags(IReadOnlyList<Token> tokens, ListParseTree tree)
        {
            int intentIndex = tree.Intent.IntentIndex;

            var flags = new List<FlagDescriptor>
            {
                new("longlisting", new[] { "-l", "--long", "longlisting" }),
                new("directoryonly", new[] { "-d", "--directoryonly", "--directory" })
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

        private NoiseExtractionResult ExtractNoise(IReadOnlyList<Token> tokens, ListParseTree tree)
        {
            var indexes = tree.GetRecognizedIndexes();
            return new NoiseExtractionResult(indexes);
        }
    }
}
