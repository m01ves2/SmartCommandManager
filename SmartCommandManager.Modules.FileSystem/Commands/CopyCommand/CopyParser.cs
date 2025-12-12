using SmartCommandManager.NLP.CommandNlp.Parsers;
using SmartCommandManager.NLP.IntentNlp.Models;

namespace SmartCommandManager.Modules.FileSystem.Commands.CopyCommand
{
    public class CopyParser : ICommandParser<CopyArgs>
    {
        IEnumerable<IValidator<CopyArgs>> _validators;

        record MarkerParseResult(IReadOnlyList<int> Indexes);
        record PathParseResult(IReadOnlyList<string> Paths);
        record FlagParseResult(bool IsPresent);
        record WildcardParseResult(bool IsWildcard);
        record NoiseParseResult(double NoiseLevel);

        record CopyParseTree(
            MarkerParseResult SourceMarker,
            MarkerParseResult DestinationMarker,
            PathParseResult SourcePaths,
            PathParseResult DestinationPaths,
            FlagParseResult IsRecursive,
            FlagParseResult IsForced,
            WildcardParseResult Wildcard,
            NoiseParseResult Noise
        );

        public CopyParser(IEnumerable<IValidator<CopyArgs>> validators)
        {
            _validators = validators.ToList();
        }

        public CopyArgs Parse(IEnumerable<Token> tokens)
        {
            throw new NotImplementedException();
        }

        public CopyArgs Parse(IReadOnlyList<Token> tokens, int intentIndex)
        {
            // 1. Построить рабочую "зону" — tokens после intent
            var span = ExtractRelevantTokens(tokens, intentIndex);

            var parseResult = new ParseResult();

            // 2. Найти ключевые слова (from, in, inside, to, into)
            parseResult = DetectMarkers(span, parseResult);

            // 3. Определить source
            string source = ExtractSource(span, markers);

            // 4. Определить destination
            string destination = ExtractDestination(span, markers);

            // 5. определить флаги
            IReadOnlyList<string> flags = ExtractFlags(span, markers);

            var copyArgs = new CopyArgs(source, destination, flags);

            // 6. Провести валидацию
            Validate(tokens, intentIndex, copyArgs, markers);

            // 7. Сформировать контекст
            return copyArgs;
        }

        private IReadOnlyList<Token> ExtractRelevantTokens(IReadOnlyList<Token> tokens, int intentIndex)
        {
            return tokens.Skip(intentIndex + 1).ToList();
        }

        private Markers DetectMarkers(IReadOnlyList<Token> tokens)
        {
            string[] sourceMarkers = ["from", "in", "inside"];
        }

        private string ExtractSource(IReadOnlyList<Token> tokens, Markers markers)
        {
            throw new NotImplementedException();
        }

        private string ExtractDestination(IReadOnlyList<Token> tokens, Markers markers)
        {
            throw new NotImplementedException();
        }

        private IReadOnlyList<string> ExtractFlags(IReadOnlyList<Token> tokens, Markers markers)
        {
            throw new NotImplementedException();
        }

        private void Validate(IReadOnlyList<Token>  tokens, int intentIndex, CopyArgs copyArgs, Markers markers)
        {
            throw new NotImplementedException();
        }

        public CopyArgs Parse(IEnumerable<Token> tokens)
        {
            throw new NotImplementedException();
        }
    }
}
