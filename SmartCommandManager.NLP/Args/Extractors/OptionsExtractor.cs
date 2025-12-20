using SmartCommandManager.NLP.Args.Models;
using SmartCommandManager.NLP.Shared.Models;
using System.Collections.ObjectModel;

namespace SmartCommandManager.NLP.Command.Extractors
{
    public static class OptionsExtractor
    {
        public static OptionsExtractionResult Extract(IReadOnlyList<Token> tokens, IReadOnlyDictionary<string, string> options)
        {
            var optionsFound = new Dictionary<string, string>();
            var indexesFound = new Collection<int>();

            for (int i = 0; i < tokens.Count; i++) {
                var token = tokens[i];
                if (!token.Value.Contains('='))
                    continue;

                var parts = token.Value.Split('=', 2);
                var name = parts[0].Trim();
                var value = parts[1].Trim();

                if (optionsFound.Keys.Contains(name) && !string.IsNullOrEmpty(value)) {
                    optionsFound.Add(name, value);
                    indexesFound.Add(i);
                }
            }
            return optionsFound.Count == 0 ? OptionsExtractionResult.Empty : new OptionsExtractionResult(optionsFound, indexesFound);
        }
    }
}